<?php

	$servername = "addemall.000webhostapp.com";
	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	$sql = $db->query("SELECT * FROM subjects");

	
	
	while($result = $sql->fetch(PDO::FETCH_ASSOC)){
		echo $result['id']."-".$result['name'].'-'.$result['id_year']."\n";
	}
	

?>