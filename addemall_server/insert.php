<?php

	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	
	$name = $_POST["name"];
	$surname = $_POST["surname"];
	$email = $_POST["email"];
	$password = $_POST["password"];
	$class = $_POST["year"];
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	
	$sql = ("INSERT INTO users (Name, Surname, email, password) VALUES ('".$name."', '".$surname."', '".$email."', '".$password."')");
	
	
    if ($db->query($sql)) {
        $sql = ("SELECT id FROM users WHERE email = '".$email."'");
        $r1 = $db->query($sql);
        $r = $r1->fetch(PDO::FETCH_ASSOC);
        $sql = ("INSERT INTO users_state (id_user, year) VALUES (".$r['id'].", ".$class.")");
        
        $r1 = $db->query($sql);
        echo "Dodano".$r['id'];
        
    } else {
        echo "Error: " . $sql . "<br>" . $db->error;
    }

	
?>