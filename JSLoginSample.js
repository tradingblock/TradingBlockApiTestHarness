var CryptoJS = require("crypto-js");
const axios = require('axios');
require('dotenv').config();

//---------------------------------------------------
//Generate Request 
//This method returns a request string that used to send to api side for a user token 
//helper function for generating IV
//helper functions for base 64 url encoding
function base64url(source) {
    // Encode in classical base64
    var encodedSource = CryptoJS.enc.Base64.stringify(source);
    // Remove padding equal characters
    encodedSource = encodedSource.replace(/=+$/, '');
    // Replace characters according to base64url specifications
    encodedSource = encodedSource.replace(/\+/g, '-');
    encodedSource = encodedSource.replace(/\//g, '_');

    return encodedSource;
}
function GenerateIV() {
    var date = new Date();
    var ivyear = date.getUTCFullYear().toString();
    var ivmonth = date.getUTCMonth() + 1 > 9 ? (date.getUTCMonth() + 1).toString() : "0" + (date.getUTCMonth() + 1);
    var ivday = date.getUTCDate() > 9 ? date.getUTCDate().toString() : "0" + date.getUTCDate();
    var ivstr = "TBAPIENT" + ivyear + ivmonth + ivday;
    return ivstr
}
//helper function for encrypt message
function Encrypt(txtUserName, _key) {
    var key = CryptoJS.enc.Utf8.parse(_key);
    var iv = CryptoJS.enc.Utf8.parse(GenerateIV());
    var ciphertext = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtUserName), key, {
        keySize: 128,
        iv: iv,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
    });
    var re = String(ciphertext.toString());
    re = re.replace(/=+$/, '');
    re = re.replace(/\+/g, '-');
    re = re.replace(/\//g, '_');
    return re;
}
//--------------------------------------------------------------------------------
// STEP 1: generate JWT header this part should provide a static output as: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9"
function generateHeader() {
    const header = {
        "typ": "JWT",
        "alg": "HS256"
    };
    var stringifiedHeader = CryptoJS.enc.Utf8.parse(JSON.stringify(header));
    var encodedHeader = base64url(stringifiedHeader);
    return encodedHeader;
}

//----------------------------------------------------
//STEP 3: Generate Signature
function generateSignature(_header, _payload, _apikey) {
    var toSign = _header + "." + _payload;
    console.log('toSign: ', toSign, 'apiKey:', _apikey);
    var signature = CryptoJS.HmacSHA256(toSign, _apikey);
    var encodedSignature = base64url(signature);
    return encodedSignature;
}


//------------------------------------------------------
//STEP 2: generate Payload
function generatePayload(_username, _password, _apikey, _bearer) {
    var user = {
        "username": _username,
        "password": _password
    };
    //encode user
    var stringedUser = JSON.stringify(user);
    //encode user json and use AES to encrypt with api key. 
    var ciphertext = Encrypt(stringedUser, _apikey);
    var date = new Date();
    var payload = {
        "EndUser": ciphertext,
        "Entity": _bearer,
        "Timestamp": date
    }
    var stringifiedPayload = CryptoJS.enc.Utf8.parse(JSON.stringify(payload));
    var encodedPayload = base64url(stringifiedPayload);
    return encodedPayload;
}


function getRequest(_username, _password, _apikey, _bearer) {
    var header = generateHeader();
    var payload = generatePayload(_username, _password, _apikey, _bearer);
    console.log('header', header);
    console.log('payload', payload);
    console.log('_apikey', _apikey);
    var signature = generateSignature(header, payload, _apikey);
    //var request = "{\"JWS\":\"" + header + "." + payload + "." + signature + "\"}";
    var request = header + "." + payload + "." + signature;
    return request;
}


 function authenticate (_payload) {
 	axios.post(process.env.AUTHURL, {
 		"JWS": _payload
 		}).then(function (response) {
    console.log(response.data);
	})
 }
console.log(process.env);

authenticate(getRequest(process.env.USER, process.env.PASSWORD, process.env.KEY, process.env.BEARER));