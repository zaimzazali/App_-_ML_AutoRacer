<?php
    function getCurrentTime() {
        return gmdate('Y-m-d h:i:s \G\M\T');
    }

    function hashString($input) {
        return password_hash($input, PASSWORD_BCRYPT);
    }
?>