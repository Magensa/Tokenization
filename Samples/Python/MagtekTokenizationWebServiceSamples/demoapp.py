#!/usr/bin/env python3.7
import json
from xml.dom.minidom import parseString
from MagtekTokenizationWebService import CreateTokens
from MagtekTokenizationWebService import RedeemTokens

#region helper methods to process user input
def read_struser_input(question,isOptional=True):
    answer = str(input(question))
    if((not isOptional) and (answer.strip() == "")):
        return read_struser_input(question,isOptional)
    return answer

def read_intuser_input(question):
    answer = str(input(question))
    try:
        int(answer)
    except ValueError:
        print("Invalid Input. Int Is Required.")
        return read_intuser_input(question)
    return answer

def read_yearuser_input(question):
    answer = str(input(question))
    if((answer.strip() == "") or (len(answer) != 4) or (not answer.isnumeric())):
        print("Invalid Year.")
        return read_yearuser_input(question)
    return int(answer)

def read_monthuser_input(question):
    answer = str(input(question))
    if((answer.strip() == "") or (len(answer) != 2) or (not answer.isnumeric()) or (int(answer) not in range(1, 13))):
        print("Invalid Input.")
        return read_monthuser_input(question)
    return int(answer)

def read_dayuser_input(question):
    answer = str(input(question))
    if((answer.strip() == "") or (len(answer) != 2) or (not answer.isnumeric()) or (int(answer) not in range(1,32))):
        print("Invalid Input.")
        return read_dayuser_input(question)
    return int(answer)

def read_houruser_input(question):
    answer = str(input(question))
    if((answer.strip() == "") or (len(answer) != 2) or (not answer.isnumeric()) or (int(answer) not in range(1,24))):
        print("Invalid Input.")
        return read_houruser_input(question)
    return int(answer)

def read_minuteuser_input(question):
    answer = str(input(question))
    if((answer.strip() == "") or (len(answer) != 2) or (not answer.isnumeric()) or (int(answer) not in range(0,60))):
        print("Invalid Input.")
        return read_minuteuser_input(question)
    return int(answer)

def read_seconduser_input(question):
    answer = str(input(question))
    if((answer.strip() == "") or (len(answer) != 2) or (not answer.isnumeric()) or (int(answer) not in range(0,60))):
        print("Invalid Input.")
        return read_seconduser_input(question)
    return int(answer)


def read_validuntilutc_input(question,yearlabel,monthlabel,daylabel,hourlabel,minutelabel,secondlabel):
    print(question)
    year = read_yearuser_input(yearlabel)
    month = read_monthuser_input(monthlabel)
    day = read_dayuser_input(daylabel)
    hour = read_houruser_input(hourlabel)
    minute = read_minuteuser_input(minutelabel)
    second = read_seconduser_input(secondlabel)

    #format to a length of 2 chars
    year = "{0:0=2d}".format(year)
    month = "{0:0=2d}".format(month)
    day = "{0:0=2d}".format(day)
    hour = "{0:0=2d}".format(hour)
    minute = "{0:0=2d}".format(minute)
    second = "{0:0=2d}".format(second)
    answer = f"{year}-{month}-{day}T{hour}:{minute}:{second}Z"
    return answer

#endregion

#region displays operation specific ui
def create_token_ui():
    
    try:
        config = json.loads(open('config.json').read())
        createtokenServiceUrl = config["createtokenurl"]
        #currently create token operation don't require certificate 
        #In future if certificate file needs to be supported. Uncomment the below lines.
        #certificateFileName = config["CERTIFICATE_FILENAME"]
        #certificatePassword = config["CERTIFICATE_PASSWORD"]
        certificateFileName = None
        certificatePassword = None
        
        print("=====================Request building start======================")
        customerTransactionId = read_struser_input("Enter Customer TransactionId:",True)
        customercode = read_struser_input("Enter Customer Code:",False)
        username = read_struser_input("Enter Username:",False)
        password = read_struser_input("Enter PassWord:",False)
        numberoftokens = read_intuser_input("Enter Number Of Tokens:")
        tokendata = read_struser_input("Enter Token Data:",True)
        tokenname = read_struser_input("Enter Token Name:",True)
        validuntilutc = read_validuntilutc_input("Enter ValidUntilUTC:", "Year(Ex:2025):-", "Month(Between 1-12)Ex:For 1 Enter 01 :-", "Day(Between 1-31)Ex: For 1 Enter 01:-", "Hour(Between 0-23)Ex: For 1 Enter 01:-", "Minute(Between 0-59)Ex: 1 Enter 01:-", "Seconds(Between 0-59)Ex:For 1 Enter 01:-")
        miscData = read_struser_input("Enter MiscData:",True)
        print("=====================Request building End======================")

        req = CreateTokens.CreateTokensRequest(customerTransactionId=customerTransactionId, customerCode=customercode,password=password,userName=username,numberOfTokens=numberoftokens,tokenData=tokendata,tokenName=tokenname,validUntilUTC=validuntilutc,miscData=miscData)

        obj = CreateTokens.CreateTokens(req)
        response = obj.CallService(createtokenServiceUrl,certificateFileName,certificatePassword)
        print('Response:-')
        print('StatusCode:' + str(response.status_code))  if (not response is None) else print('StatusCode:' + str(response))
        print('Content:\n' + parseString(response.content).toprettyxml())  if (not response is None) else print('Content:' + str(response))
    except Exception as ex:
        print(ex)

def redeem_token_ui():
    
    try:
        config = json.loads(open('config.json').read())
        redeemtokenServiceUrl = config["redeemtokenurl"]
        certificateFileName = config["CERTIFICATE_FILENAME"]
        certificatePassword = config["CERTIFICATE_PASSWORD"]
        
        print("=====================Request building start======================")
        customercode = read_struser_input('Enter Customer Code:',False)
        username = read_struser_input("Enter Username:",False)
        password = read_struser_input("Enter PassWord:",False)
        customerTransactionID = read_struser_input("Enter CustomerTransactionID:",True)
        token = read_struser_input("Enter Token:",False)
        print("=====================Request building End======================")

        req = RedeemTokens.RedeemTokensRequest(customerCode=customercode,Password=password,userName=username,token=token,customerTransactionID=customerTransactionID)
        obj = RedeemTokens.RedeemTokens(req)
        response = obj.CallService(redeemtokenServiceUrl,certificateFileName,certificatePassword)
        print('Response:-')
        print('StatusCode:' + str(response.status_code))  if (not response is None) else print('StatusCode:' + str(response))
        print('Content:\n' + parseString(response.content).toprettyxml())  if (not response is None) else print('Content:' + str(response))
    except Exception as ex:
        print(ex)


#endregion

# method for choosing any option
def choose_option():
    print("Please Select Service Option Number")
    print("1:Create Tokens")
    print("2:Redeem Token")
    input_option = input()
    if(input_option.isdigit()):
        main(str(input_option)) 
    else:
        print("Please enter only interger value")
        choose_option()

# go to particular option and process
def main(opt):
    option = int(opt)
    if option == 1:
        create_token_ui()
        choose_option()
    elif option == 2:
        redeem_token_ui()
        choose_option()
    else:
        print("Invalid Option")
     

if __name__ == "__main__":
    choose_option()
