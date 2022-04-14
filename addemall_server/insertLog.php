<?php

	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	
	$id = $_POST["id"];
	$date = $_POST["start"];
	$duration = $_POST["duration"];
	
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	
	$sql = ("INSERT INTO loginfo_users (id_user, date, duration) VALUES ('".$id."', '".$date."', '".$duration."')");
	
    
    if (!$db->query($sql)) {
        echo "Error: " . $sql . "<br>" . $db->error;
    }

	
?>