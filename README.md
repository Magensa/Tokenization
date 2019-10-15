# Tokenization-PII
Cloud Service to Secure and Privatize Personally Identifiable Information (PII)

Magensa is a cloud-based service.  We are experts in encryption key management, hardware security modules, authentication, cryptographic systems, and tokenization.   Our solutions employ symmetric key encryption using AES/3DES by DUKPT (Derived Unique Key per Transaction) – meaning a unique key is used for every data element sent and encrypted.     
 
The Magensa Tokenization Service is Payment Card Industry (PCI) compliant. After protecting credit card data and serving the payment industry faithfully, Magensa has developed trusted, high-performance services that enable developers to build tokenization into applications to protect sensitive data from its intake or batch protect legacy data stores.  Magensa provides true vaultless tokenization.  This means when sensitive data is sent to Magensa to be tokenized, the client receives back a dynamically encrypted, self-contained token.  Magensa stores nothing.   The token is constructed with a cryptogram of the sensitive data, key metadata added based on the token template, and a cryptographic hash of the entire payload for integrity.  The entire cryptographic blob is returned.  The organization maintains control of its sensitive data in a non-identifiable form and economically worthless state.  Magensa tokens are of no use to criminals if your environment is compromised

This web service enables developers to send data to Magensa for token creation.  The encrypted, self-contained token is returned to the client to manage/store.  Through the redeem API, tokens are sent to Magensa for decription, data manipulation if defined, and either returned or securely forwarded to customer defined endpoints.

Getting Started
A test account must be set-up in order to utilize the Magensa Tokenization Service.  Please contact Magensa Sales at service.solutions@magtek.com in order to acquire test account credentials, a client certificate, and IP whitelisting.
