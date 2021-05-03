
# Introduction
The repository contains demoapp.py for operations in the tokenization web service. Below are the list of operations.

	1.CreateTokens
	2.RedeemToken

# Clone the repository
 1. Navigate to the main page of the  **repository**. 
 2. Under the  **repository**  name, click  **Clone** .
 3. Use any Git Client(eg.: GitBash, Git Hub for windows, Source tree ) to  **clone**  the  **repository**  using HTTPS.

Note: reference for  [Cloning a Github Repository](https://help.github.com/en/articles/cloning-a-repository)

# Getting Started

1. Install latest version of Python
	Install Python from [Python Org Site](https://www.python.org/), Make sure python is in your path,and set python and scripts folder to the path variables. 
	Install Virtual Environment using the command		
  ```pip install virtualenv ```    

2. Create a virtual environment with name of your choice (eg:"magtekwebservicesdemoenv") 
	 run the following command 

	 ```python -m venv e:\pythonenvs\magtekwebservicesdemoenv```
  
3. How to activate/deactivate the virtual environment.    
	    
	On windows, the equivalent `activate` script is in the `Scripts` folder "<<path to magtekwebservicesdemoenv\Scripts>>" and
	 run:  ``activate`` to activate environment
	 and type `deactivate` to undo the changes.
		
If using powershell, the `activate` script is subject to the [Execution policies](http://technet.microsoft.com/en-us/library/dd347641.aspx) on the system. By default on Windows 7, the system’s execution policy is set to `Restricted`, meaning no scripts like the `activate` script are allowed to be executed. But that can’t stop us from changing that slightly to allow it to be executed.
	In order to use the script, you can relax your system’s execution policy to `AllSigned`, meaning all scripts on the system must be digitally signed to be executed.  As an administrator run:
	```PS C:\> Set-ExecutionPolicy AllSigned```

	Since the `activate.ps1` script is generated locally for each virtualenv, it is not considered a remote script and can then be executed.
 And type `deactivate.ps1` to undo the changes.
 Identifying the terminals when environment is active by the below change in editor
 eg : -  
	 Before active in terminal 
  ```PS C:\WINDOWS\system32>```
  After activated in terminal
	```(magtekwebservicesdemoenv) PS C:\WINDOWS\system32>```
4. Installing the required python packages
				
		Go to the directory path from command line where ''requirements.txt'' is located.   
	run: ```pip install -r requirements.txt``` in your shell.

	***Note***:	In a newly created virtualenv there will also be a **activate** shell script. For Windows systems, activation scripts are provided for the command line	
5. Software dependencies
	- Latest version of python - at the time of writing code that is "Python 3.7.4"
	
	***Note*** : RedeemToken operation in tokenization service is dependent on SSL Certificate.Test certificate is available in cloned folder.
    
6. Latest releases

	- Initial release with all commits and changes as on October 17th 2019.

# Build and Run

Steps to run demoapp

- Navigate to the virtual environment path from the command prompt and activate the virtual environment. 
- Go to scripts folder path and run ``Activate`` command, and editor will open up virtual environment as below
```(magtekwebservicesdemoenv) PS C:\WINDOWS\system32>```  
- Now go to project folder downloaded path from the above command line
```Cd C:\MagtekTokenizationWebServiceSamples``` and run command as  ```python demoapp.py```
This should open the application running in Console.
