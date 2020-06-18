<?php
$__db_host = 'localhost';
$__db_user = 'pugpawzc';
$__db_pass = '@NO12W4N4VA:443';
$__db_name = 'pugpawzc_dashie';
$__db_charset = 'utf8';

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

$dsn = "mysql:host=$__db_host;dbname=$__db_name;charset=$__db_charset";

$opt = [
    PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_LAZY,
    PDO::ATTR_EMULATE_PREPARES   => false,
];

$db = new PDO($dsn, $__db_user, $__db_pass);
?>
