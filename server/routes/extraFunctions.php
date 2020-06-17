<?php
    function getCurrentTime() {
        return gmdate('Y-m-d h:i:s \G\M\T');
    }

    function hashString($input) {
        return password_hash($input, PASSWORD_BCRYPT);
    }

    function encodeString($input) {
        return urlencode($input);
    }

    function decodeString($input) {
        return urldecode($input);
    }
?>