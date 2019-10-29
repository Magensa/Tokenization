const fs = require("fs");
const readln = require("readline");
const path = require("path");
const isNumber = require("is-number");
const formattoxml = require("xml-formatter");
const CreateTokens = require("./CreateTokens");
const RedeemToken = require("./RedeemToken");

let cl = readln.createInterface(process.stdin, process.stdout);

//helper functions to read user inputs
const Answer = function (q) {
    return new Promise((resolve) => {
        cl.question(q, answer => {
            resolve(answer);
        });
    });
};

async function read_struser_input(question, IsOptional = true) {
    let ans = await Answer(question);
    if ((!IsOptional) && (ans.trim() === "")) {
        return read_struser_input(question, IsOptional);
    }
    return ans;
}

async function read_intuser_input(question) {
    let ans = await Answer(question);

    if ((ans.trim === "") || (!isNumber(ans))) {
        return read_intuser_input(question);
    } else {
        return ans;
    }
}

function between(n, a, b) {
    return ((n >= a) && (n <= b));
}

async function read_yearuser_input(question) {
    let ans = await read_struser_input(question, false);
    if ((isNumber(ans)) && (parseInt(ans)) && (ans.length === 4)) {
        return ans;
    } else {
        console.log("Invalid Input.");
        return read_yearuser_input(question);
    }
}

async function read_monthuser_input(question) {
    let ans = await read_struser_input(question, false);
    if ((isNumber(ans)) && (parseInt(ans)) && (ans.length === 2) && (between(parseInt(ans), 1, 12))) {
        return ans;
    } else {
        console.log("Invalid Input.");
        return read_monthuser_input(question);
    }
}

async function read_dayuser_input(question) {
    let ans = await read_struser_input(question, false);
    if ((isNumber(ans)) && (parseInt(ans)) && (ans.length === 2) && (between(parseInt(ans), 1, 31))) {
        return ans;
    } else {
        console.log("Invalid Input.");
        return read_dayuser_input(question);
    }
}

async function read_houruser_input(question) {
    let ans = await read_struser_input(question, false);
    if ((isNumber(ans)) && (parseInt(ans)) && (ans.length === 2) && (between(parseInt(ans), 0, 23))) {
        return ans;
    } else {
        console.log("Invalid Input.");
        return read_houruser_input(question);
    }
}

async function read_minuteuser_input(question) {
    let ans = await read_struser_input(question, false);
    if ((isNumber(ans)) && (parseInt(ans)) && (ans.length === 2) && (between(parseInt(ans), 0, 59))) {
        return ans;
    } else {
        console.log("Invalid Input.");
        return read_minuteuser_input(question);
    }
}

async function read_secondsuser_input(question) {
    let ans = await read_struser_input(question, false);
    if ((isNumber(ans)) && (parseInt(ans)) && (ans.length === 2) && (between(parseInt(ans), 0, 59))) {
        return ans;
    } else {
        console.log("Invalid Input.");
        return read_secondsuser_input(question);
    }
}


async function read_validuntilutc_input(question, yearlabel, monthlabel, daylabel, hourlabel, minutelabel, secondslabel) {
    console.log(question);
    let year = await read_yearuser_input(yearlabel);
    let month = await read_monthuser_input(monthlabel);
    let day = await read_dayuser_input(daylabel);
    let hours = await read_houruser_input(hourlabel);
    let minutes = await read_minuteuser_input(minutelabel);
    let seconds = await read_secondsuser_input(secondslabel);
    return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}Z`;
}



//decrypt card swipe ui
async function createtokens_ui() {

    let configSettings = fs.readFileSync("config.json");
    let config = JSON.parse(configSettings);
    let createTokensSvcUrl = config.createTokensSvcUrl;
    let certificateFileName = config.certificateFileName;
    let certificatePassword = config.certificatePassword;
    let currentDir = path.resolve(__dirname, "./");
    let certificateFilePath = path.join(currentDir, certificateFileName);

    console.log("=====================Request building start======================");
    let customerTransactionId = await read_struser_input("Enter CustomerTransactionID:", true);
    let customerCode = await read_struser_input("Enter CustomerCode:", false);
    let userName = await read_struser_input("Enter UserName:", false);
    let password = await read_struser_input("Enter Password:", false);
    let numberOfTokens = await read_intuser_input("Enter NumberOfTokens:", false);
    let tokenData = await read_struser_input("Enter TokenData:", true);
    let tokenName = await read_struser_input("Enter TokenName:", true);
    let validUntilUTC = await read_validuntilutc_input("Enter ValidUntilUTC:", "Year(Ex:2025):-", "Month(Between 1-12)Ex:For 1 Enter 01 :-", "Day(Between 1-31)Ex: For 1 Enter 01:-", "Hour(Between 0-23)Ex: For 1 Enter 01:-", "Minute(Between 0-59)Ex: 1 Enter 01:-", "Seconds(Between 0-59)Ex:For 1 Enter 01:-");
    let miscData = await read_struser_input("Enter MiscData", true);

    console.log("=====================Request building End======================");
    let obj = new CreateTokens(customerTransactionId, customerCode, userName, password, numberOfTokens, tokenData, tokenName, validUntilUTC, miscData);

    try {
        let response = await obj.CallWebService(createTokensSvcUrl, certificateFilePath, certificatePassword);
        console.log("Response StatusCode Is:-");
        console.log(response.statuscode);
        console.log("Response Is:-");
        console.log(formattoxml(response.data));
    }
    catch (ex) {
        console.log("Exception:-");
        console.log(ex.message);
    }
}


//decrypt data ui
async function redeemtoken_ui() {

    let configSettings = fs.readFileSync("config.json");
    let config = JSON.parse(configSettings);
    let redeemTokenSvcUrl = config.redeemTokenSvcUrl;
    let certificateFileName = config.certificateFileName;
    let certificatePassword = config.certificatePassword;
    let currentDir = path.resolve(__dirname, "./");
    let certificateFilePath = path.join(currentDir, certificateFileName);

    console.log("=====================Request building start======================");
    let customertransactionid = await read_struser_input("Enter CustomerTransactionID:", true);
    let customercode = await read_struser_input("Enter CustomerCode:", false);
    let username = await read_struser_input("Enter UserName:", false);
    let password = await read_struser_input("Enter Password:", false);
    let token = await read_struser_input("Enter Token:", false);

    let obj = new RedeemToken(customertransactionid, customercode, username, password, token);

    try {
        let response = await obj.CallWebService(redeemTokenSvcUrl, certificateFilePath, certificatePassword);
        console.log("Response StatusCode Is:-");
        console.log(response.statuscode);
        console.log("Response Is:-");
        console.log(formattoxml(response.data));
    }
    catch (ex) {
        console.log("Exception:-");
        console.log(ex.message);
    }
}

//main function
(async () => {
    for (; ;) {
        console.log("Please Select Service Option Number (1:CreateTokens,2:RedeemToken)");
        let opt = await read_struser_input("Enter Option:", true);
        if (isNumber(opt)) {
            let option = parseInt(opt);
            switch (option) {
                case 1:
                    await createtokens_ui();
                    break;
                case 2:
                    await redeemtoken_ui();
                    break;
                default:
                    console.log("Invalid Option");
                    cl.close();
                    process.exit();
            }
        }
        else {
            console.log("Enter Numbers Only");
        }
    }
})();