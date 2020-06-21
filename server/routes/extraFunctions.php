<?php
    function getCurrentTime() {
        return gmdate('Y-m-d h:i:s \G\M\T');
    }

    function hashString($input) {
        return password_hash($input, PASSWORD_BCRYPT);
    }

    function isHashedStringSimilar($input1, $input2) {
        return password_verify($input1, $input2);
    }

    function encodeString($input) {
        return urlencode($input);
    }

    function decodeString($input) {
        return urldecode($input);
    }

    function convertFetchAllIntoArrayTable($result) {
        $finalResult = "";
        $tmpVal = null;

        foreach ($result as &$row) {
            $len = count($row);
            $tmpStr = "";
            for ($i=0; $i<$len; $i++) {
                if ($row[$i] != "") {
                    $tmpVal = convertIfBool($row[$i]);
                    $col = decodeString($tmpVal).",";
                    $tmpStr .= $col;
                }
            }
            $lastPosition = strrpos($tmpStr, ",");
            $finalResult .= substr($tmpStr, 0, $lastPosition).";";
        }

        $lastPosition = strrpos($finalResult, ";");
        $finalResult = substr($finalResult, 0, $lastPosition);

        return $finalResult;
    }

    function convertFetchIntoArray($colCount, $result) {
        $finalResult = "";
        $tmpVal = null;

        for ($i=0; $i<$colCount; $i++) {
            $tmpVal = convertIfBool($result[$i]);
            $finalResult .= decodeString($tmpVal).",";
        }
        $lastPosition = strrpos($finalResult, ",");
        $finalResult = substr($finalResult, 0, $lastPosition);

        return $finalResult; 
    }

    function convertIfBool($result) {
        if ($result === true) {
            return "true";
        } elseif ($result === false) {
            return "false";
        } else {
            return $result;
        }
    }
?>