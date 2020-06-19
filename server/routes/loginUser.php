<?php
// $_POST["isAllowed"] = "true";
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------
    if ($_POST["isAllowed"] != "true") {
        header('Location: http://localhost:1111');
    }
// ------------------------------------------------------------------------- 
// -------------------------------------------------------------------------
    include "dbCredentials.php";
    include "extraFunctions.php";

    // $_POST['theUsername'] = "test20";
    // $_POST['thePassword'] = "zaimzaim";

    
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
                // $array = ["not exist", false];
                $myObj->signal = "not exist";
            } elseif (!$result['account_active']) {
                // $array = ["not active", false];
                $myObj->signal = "not active";
            } elseif (isHashedStringSimilar($_POST['thePassword'], $hashedPassword)) {
                // $array = ["OK", $result];
                $myObj->signal = "OK";
            } else {
                // $array = ["invalid", false];
                $myObj->signal = "invalid";
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