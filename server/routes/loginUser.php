<?php
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------
    if ($_POST["isAllowed"] != "true") {
        echo "ERROR";
        header('Location: http://localhost:1111');
    }
// ------------------------------------------------------------------------- 
// -------------------------------------------------------------------------
    include "dbCredentials.php";
    include "extraFunctions.php";

    try {
        $myPDO = new PDO("pgsql:host=$db_host;port=$db_port;dbname=$db_name", $db_user, $db_pass);

        try {
            
            $myPDO->beginTransaction();

            $sql = "SELECT account_active, account_type, account_password, password_require_reset, code_user_details FROM view_user_account WHERE username = :theUsername;";
        
            $stmt = $myPDO->prepare($sql);
            $stmt->bindValue(':theUsername', encodeString($_POST['theUsername']));

            $stmt->execute();
            $result = $stmt->fetch();
            $rowCount = $stmt->rowCount();
            $colCount = $stmt->columnCount();

            $myPDO->commit();
            
            $hashedPassword = $result['account_password'];

            if ($rowCount == 0) {
                echo "not exist";
            } elseif (!$result['account_active']) {
                echo "not active";
            } elseif (isHashedStringSimilar($_POST['thePassword'], $hashedPassword)) {
                echo "OK".";".convertFetchIntoArray($colCount, $result);
            } else {
                echo "invalid";
            }
                       
        } catch (PDOException $e2) {
            $myPDO->rollBack();
            echo $e2->getMessage();
        }

        $myPDO = null;
    } catch (PDOException $e1) {
        echo $e1->getMessage();
    }
?>