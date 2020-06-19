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

            $myPDO->commit();
            
            $hashedPassword = $result['account_password'];

            if ($rowCount == 0) {
                $myObj->signal = "not exist";
                $myObj->data = null;
            } elseif (!$result['account_active']) {
                $myObj->signal = "not active";
                $myObj->data = null;
            } elseif (isHashedStringSimilar($_POST['thePassword'], $hashedPassword)) {
                $myObj->signal = "OK";
                $myObj->data = $result;
            } else {
                $myObj->signal = "invalid";
                $myObj->data = null;
            }

            echo json_encode($myObj);
        } catch (PDOException $e2) {
            $myPDO->rollBack();
            echo $e2->getMessage();
        }

        $myPDO = null;
    } catch (PDOException $e1) {
        echo $e1->getMessage();
    }
?>