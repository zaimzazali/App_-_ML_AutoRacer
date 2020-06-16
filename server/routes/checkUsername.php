<?php
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------
    if ($_POST["isAllowed"] != "true") {
        header('Location: http://localhost:1111');
    }
// ------------------------------------------------------------------------- 
// -------------------------------------------------------------------------
    include "dbCredentials.php";

    try {
        $myPDO = new PDO("pgsql:host=$db_host;port=$db_port;dbname=$db_name", $db_user, $db_pass);

        try {
            $myPDO->beginTransaction();

            $sql = "SELECT count(1) as c FROM view_user_account WHERE username = :theUsername;";
        
            $stmt = $myPDO->prepare($sql);
            $stmt->bindValue(':theUsername', $_POST['theUsername']);

            $stmt->execute();
            $result = $stmt->fetch();

            $myPDO->commit();

            $isAvailable = $result['c'];

            if ($isAvailable == 0) {
                echo "available";
            } elseif ($isAvailable == 1) {
                echo "not available";
            } else {
                echo "ERROR";
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