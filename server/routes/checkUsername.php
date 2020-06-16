<?php
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------
    if ($_POST["isAllowed"] != "true") {
        header('Location: http://localhost:1111');
    }
// ------------------------------------------------------------------------- 
// -------------------------------------------------------------------------
    include "dbCredentials.php";

    echo $_POST['thisUsername'];
    echo "</br>";

    try {
        $myPDO = new PDO("pgsql:host=$db_host;port=$db_port;dbname=$db_name", $db_user, $db_pass);

        try {
            $myPDO->beginTransaction();

            $sql = 'SELECT count(1) as c FROM view_user_account WHERE username = :thisUsername;';
        
            $stmt = $myPDO->prepare($sql);
            $stmt->bindValue(':thisUsername', $_POST['thisUsername']);

            $stmt->execute();
            $result = $stmt->fetch();

            if ($result['c'] == 1) {
                echo 'not available';
            }
            else if ($result['c'] == 0) {
                echo 'available';
            }

            $myPDO->commit();
        } catch (PDOException $e2) {
            $myPDO->rollBack();
            echo $e2->getMessage();
        }

        $myPDO = null;
    } catch (PDOException $e1) {
        echo $e1->getMessage();
    }
?>