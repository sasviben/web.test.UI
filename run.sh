﻿#!/bin/bash
#----TEST RUNS----
# regression test run ID: 513396
# acceptance test run ID: 514338
# smoke test run ID: 514339
#----EXECUTION ENVIRONMENT----
# Silent
# Stage
# QA

# dodati opis u word

TEST_RUN_ID="513396" #test run-a id koji omogućuje aplikaciji da povuće test scenarije koje treba izvršiti
TEST_CONFIG="UI\Features\hiptest-publisher.conf" #konfiguracija koja omogućuje komunikaciju između CucumberStudia i aplikacije
TEST_ENVIRONMENT="QA" #okolina test run-a
#TEST_FILTER="regression"
TEST_FILTER="TestCategory=RULES_automated" #scenariji koji sadrže tag koji odgovara navedenom filteru će biti izvršeni
environment="QA" #okolina na kojoj će se testovi izvršiti
browser="Chrome" #internet preglednik u kojem će se izvršiti testovi
# Fetch tests
if ! hiptest-publisher \
	--config-file "$TEST_CONFIG" \
	--test-run-id "$TEST_RUN_ID" \
	--execution-environment "$TEST_ENVIRONMENT" } \
	--only features
then
	echo "Unable to fetch tests, exiting!"
fi
# Run tests
dotnet test \
	-c Debug \
    --filter "$TEST_FILTER" \
    --logger 'trx;LogFileName=TESTRESULTS.xml'

# Publish the test results
if ! hiptest-publisher \
    --config-file "$TEST_CONFIG" \
    --push UI/TestResults/TESTRESULTS.xml \
    --test-run-id "$TEST_RUN_ID" \
    --execution-environment "$TEST_ENVIRONMENT" \
    --push-format mstest
then
	echo "Unable to push test results, exiting!"
fi
environment=" " 