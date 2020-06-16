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
    $hashedPassword = hashString($_POST["isAllowed"]);
    $currentTime = getCurrentTime();

    // Parse integer
    $theGender = (int)$_POST["theGender"];
    $theCountry = (int)$_POST["theCountry"];

    echo $_POST["theName"]; echo "<br>";
    echo $theGender; echo "<br>";
    echo $_POST["theYear"]; echo "<br>";
    echo $theCountry; echo "<br>";
    echo $_POST["theUsername"]; echo "<br>";
    echo $_POST["theEmail"]; echo "<br>";
    echo $hashedPassword; echo "<br>";
    echo $currentTime; echo "<br>";
/*
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
    */
?>