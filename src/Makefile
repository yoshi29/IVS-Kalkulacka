FNAME = xstopk01_xvagne08_xmatus34

all: prepare run

prepare:
	dotnet build --configuration Release --nologo .\Calculator\Calculator.sln

pack: clean
	if exist ..\..\$(FNAME) rmdir /s /q ..\..\$(FNAME) 
	mkdir ..\..\$(FNAME)\doc
	mkdir ..\..\$(FNAME)\install
	mkdir ..\..\$(FNAME)\repo

#TODO: Copy generated program documentation

	if exist .\Calculator\Calculator.Installer\Calculator-Release-x86.msi copy .\Calculator\Calculator.Installer\Calculator-Release-x86.msi ..\..\$(FNAME)\install\Calculator-Release-x86.msi
	if exist .\Calculator\Calculator.Installer\Calculator-Release-x64.msi copy .\Calculator\Calculator.Installer\Calculator-Release-x64.msi ..\..\$(FNAME)\install\Calculator-Release-x64.msi

	xcopy /s ..\. "..\..\$(FNAME)\repo\" 

	powershell Compress-Archive -Update ..\..\$(FNAME) ..\..\$(FNAME).zip
	if exist ..\..\$(FNAME) rmdir /s /q ..\..\$(FNAME) 

clean:
	if exist .\Calculator\.vs attrib -h .\Calculator\.vs
	if exist .\Calculator\.vs rmdir /s /q .\Calculator\.vs
	
	if exist .\Calculator\Calculator\Calculator.csproj.user del /Q .\Calculator\Calculator\Calculator.csproj.user

	if exist .\Calculator\Calculator\bin rmdir /s /q .\Calculator\Calculator\bin
	if exist .\Calculator\Calculator\obj rmdir /s /q .\Calculator\Calculator\obj
	
	if exist .\Calculator\ExpressionProcessor\bin rmdir /s /q .\Calculator\ExpressionProcessor\bin
	if exist .\Calculator\ExpressionProcessor\obj rmdir /s /q .\Calculator\ExpressionProcessor\obj
	
	if exist .\Calculator\MathLibrary\bin rmdir /s /q .\Calculator\MathLibrary\bin
	if exist .\Calculator\MathLibrary\obj rmdir /s /q .\Calculator\MathLibrary\obj
	
	if exist .\Calculator\MathLibraryTests\bin rmdir /s /q .\Calculator\MathLibraryTests\bin
	if exist .\Calculator\MathLibraryTests\obj rmdir /s /q .\Calculator\MathLibraryTests\obj
	
	if exist .\Calculator\StandardDeviation\bin rmdir /s /q .\Calculator\StandardDeviation\bin
	if exist .\Calculator\StandardDeviation\obj rmdir /s /q .\Calculator\StandardDeviation\obj
	
	if exist .\Calculator\Calculator.Installer\bin\Release\Calculator-Release-x86.msi move .\Calculator\Calculator.Installer\bin\Release\Calculator-Release-x86.msi .\Calculator\Calculator.Installer\Calculator-Release-x86.msi
	if exist .\Calculator\Calculator.Installer\bin\Release\Calculator-Release-x64.msi move .\Calculator\Calculator.Installer\bin\Release\Calculator-Release-x64.msi .\Calculator\Calculator.Installer\Calculator-Release-x64.msi
	if exist .\Calculator\Calculator.Installer\bin rmdir /s /q .\Calculator\Calculator.Installer\bin
	if exist .\Calculator\Calculator.Installer\obj rmdir /s /q .\Calculator\Calculator.Installer\obj
	
run: prepare
	Calculator\Calculator\bin\Release\netcoreapp3.1\Calculator.exe

profile: prepare
	Calculator\StandardDeviation\bin\Release\netcoreapp3.1\StandardDeviation.exe -r

help:
	echo You must install .NET Core SDK 3.1 before running the program.
