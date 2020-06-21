<?php
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------
    if ($_POST["isAllowed"] != "true") {
        header('Location: http://localhost:1111');
    }
// ------------------------------------------------------------------------- 
// -------------------------------------------------------------------------
    include "dbCredentials.php";
    include "extraFunctions.php";

    // Encrypt the password and get current time
    $hashedPassword = hashString($_POST["thePassword"]);
    $currentTime = getCurrentTime();

    // Parse integer
    $theGender = (int)$_POST["theGender"];
    $theCountry = (int)$_POST["theCountry"];

    // Encode strings
    $theName = encodeString($_POST["theName"]);
    $theUsername = encodeString($_POST["theUsername"]);
    $theEmail = encodeString($_POST["theEmail"]);

    try {
        $myPDO = new PDO("pgsql:host=$db_host;port=$db_port;dbname=$db_name", $db_user, $db_pass);

        try {
            $myPDO->beginTransaction();

            // Insert into list_details
            $sql = "INSERT INTO list_details (user_name, code_gender, year_of_birth, code_country, user_email, last_update) 
                    VALUES (:theName, :theGender, :theYear, :theCountry, :theEmail, :currentTime);";

            $stmt = $myPDO->prepare($sql);
            $stmt->bindValue(':theName', $theName);
            $stmt->bindValue(':theGender', $theGender);
            $stmt->bindValue(':theYear', $_POST["theYear"]);
            $stmt->bindValue(':theCountry', $theCountry);
            $stmt->bindValue(':theEmail', $theEmail);
            $stmt->bindValue(':currentTime', $currentTime);

            $stmt->execute();
            $id_details = $myPDO->lastInsertId();

            // Insert into list_password
            $sql = "INSERT INTO list_password (user_password, need_reset, last_update) 
                    VALUES (:hashedPassword, false, :currentTime);";

            $stmt = $myPDO->prepare($sql);
            $stmt->bindValue(':hashedPassword', $hashedPassword);
            $stmt->bindValue(':currentTime', $currentTime);

            $stmt->execute();
            $id_password = $myPDO->lastInsertId();

            // Insert into list_user
            $sql = "INSERT INTO list_user (username, is_active, code_account_type, code_password, code_details) 
                    VALUES (:theUsername, true, 1, :id_password, :id_details);";

            $stmt = $myPDO->prepare($sql);
            $stmt->bindValue(':theUsername', $theUsername);
            $stmt->bindValue(':id_password', $id_password);
            $stmt->bindValue(':id_details', $id_details);

            $stmt->execute();

            $myPDO->commit();

            echo "OK";
        } catch (PDOException $e2) {
            $myPDO->rollBack();
            echo $e2->getMessage();
        }

        $myPDO = null;
    } catch (PDOException $e1) {
        echo $e1->getMessage();
    }
?>