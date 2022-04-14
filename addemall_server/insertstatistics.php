<?php

	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	
	$user = $_POST["user"];
	$question = $_POST["question"];
	$answered = $_POST["answered"];
	$time = $_POST["time"];
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	
	
	
	$sql = ("INSERT INTO results (id_player, id_question, answered, time) VALUES (".$user." ,".$question.", '".$answered."', '".$time."')");
	
    $db->query($sql);
    
	
?>