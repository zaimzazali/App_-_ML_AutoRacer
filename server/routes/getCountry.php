<?php
// -------------------------------------------------------------------------
// -------------------------------------------------------------------------
    if ($_POST["isAllowed"] != "true") {
        echo json_encode("ERROR");
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

            $sql = "SELECT * FROM list_country;";
            $stmt = $myPDO->prepare($sql);

            $stmt->execute();
            $result = $stmt->fetchAll();

            $myPDO->commit();

            echo json_encode($result);
        } catch (PDOException $e2) {
            $myPDO->rollBack();
            echo $e2->getMessage();
        }

        $myPDO = null;
    } catch (PDOException $e1) {
        echo $e1->getMessage();
    }
?>