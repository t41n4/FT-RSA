### **Summary: RSA Data Transmission and Storage Simulation with Third-Party Infrastructure**

In this personal project, I developed a simulation of secure data transmission using the **RSA (Rivest–Shamir–Adleman)** public-key cryptosystem, with the added complexity of using a third-party infrastructure to store encrypted files and an SQL database to manage RSA keys and file metadata.

The project consists of several key components:

1. **Key Generation**: The RSA key generation process is implemented, where a pair of public and private keys are created using large prime numbers. These keys are securely stored in a SQL database, ensuring that the private keys are kept confidential while the public keys can be shared.

2. **Encryption**: The project simulates a secure data transmission system where the sender uses the recipient's **public key** to encrypt files before transmission. These encrypted files are then uploaded to a third-party cloud storage infrastructure, ensuring that the encrypted data is stored securely.

3. **SQL Database for Key and Metadata Storage**: The RSA keys (public and private) are securely stored in an SQL database along with metadata associated with the files. The database stores crucial information such as:
   - **File ID**: A unique identifier for each file.
   - **Public Key ID**: A reference to the recipient's public key used for encryption.
   - **File Metadata**: Information about the file, such as file name, size, and encryption status.

4. **Decryption and Retrieval**: When the recipient needs to access the encrypted file, the application retrieves the encrypted file from the third-party infrastructure and fetches the corresponding private key from the SQL database. The file is then decrypted using the private key, allowing the recipient to access the original data.

5. **Digital Signatures**: To further secure the file and ensure its integrity, I incorporated a digital signature mechanism. The sender digitally signs the encrypted file using their private key, and the recipient can verify the signature using the sender's public key, ensuring that the file has not been tampered with during storage or transmission.

By leveraging third-party infrastructure for secure file storage and using an SQL database for key and metadata management, the project demonstrates a comprehensive approach to secure data transmission, ensuring both confidentiality and integrity throughout the process. This setup simulates real-world scenarios where sensitive data is encrypted, stored, and managed securely in cloud environments, while utilizing RSA encryption for privacy protection.
