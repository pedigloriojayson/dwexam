
SQL and File Requirements:
	
	1. Postgresql service running on the local machine (localhost, or either change the configuration on appsettings.json ConnectionStrings:DatabaseConnection)
	2. Postgresql username/password (default by username: postgres, password: password123) or you may change the configuration on apsettings.json.
		2.1 Access should have rights to create and truncate table
		2.2 Access should have rights to select and insert data in the tables.
	3. Postgresql database "TestDB" should be existing on the localhost database, or you may change the configuration on apsettings.json.
	4. .csv file should locate on .exe base path.
		4.1 .csv file default name covid_19_data.csv or you may change file name on appsettings.json

Application Requirements:

	1. Libraries and Main API is built in .NET Framework 6.0
	2. Machine port 7234 (https) and 5234 (http) will be used for hosting web services, can be change on launchSettings.json


Solution Requirements and Dependencies:
	
	1. DWCommon
		- Npgsql (6.0.3)
		- Microsoft.Extensions.Configuration.Json 6.0
	2. DWApp
		- DWCommon
		- Npgsql (6.0.3)
	3. DWInfra
		- DWCommon
		- Npgsql (6.0.3)
	4. DWTechExam
		- Swashbuckle.AspNetCore 6.2.3 (for api documentation, can be disabled).
		- DWCommon
		- DWApp
		- DWInfra


Sample URL GET request: https://localhost:7234/top/confirmed?observation_date=2020-01-22&max_results=10