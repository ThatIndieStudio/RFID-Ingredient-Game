#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN 10
#define RST_PIN 9

MFRC522 mfrc522(SS_PIN, RST_PIN);

void setup() {
  Serial.begin(9600);
  SPI.begin();
  mfrc522.PCD_Init();
   mfrc522.PCD_SetAntennaGain(mfrc522.RxGain_max);
  Serial.println("Ready to read RFID tags!");
}

void loop() {
  if (mfrc522.PICC_IsNewCardPresent() && mfrc522.PICC_ReadCardSerial()) {
    MFRC522::MIFARE_Key key;
    byte blockAddr = 4; // Block address where the ASCII values are stored

    // Prepare the key
    for (byte i = 0; i < 6; i++) {
      key.keyByte[i] = 0xFF;
    }

    // Authenticate using key A
    if (mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, blockAddr, &key, &(mfrc522.uid)) == MFRC522::STATUS_OK) {
      byte buffer[18];
      byte bufferSize = sizeof(buffer);

      // Read the block containing the ASCII values
      if (mfrc522.MIFARE_Read(blockAddr, buffer, &bufferSize) == MFRC522::STATUS_OK) {
        // Convert the ASCII values to characters and form the word "dog"
        String tagData = "";
        for (byte i = 0; i < bufferSize; i++) {
        if (isAlpha(buffer[i])) { // Check if the byte is an alphabetic character
            tagData += char(buffer[i]);
          }
        }
        // Print the word "dog" read from the tag
        Serial.println(tagData);
      } else {
        Serial.println("Error reading data from the tag!");
      }

      mfrc522.PICC_HaltA();
      mfrc522.PCD_StopCrypto1();
    } else {
      Serial.println("Authentication failed!");
    }
  }
}
