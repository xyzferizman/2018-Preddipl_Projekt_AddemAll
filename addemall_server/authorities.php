<?php

	$servername = "addemall.000webhostapp.com";
	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	$admin = $_POST["admin_id"];
	
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	$sql = $db->query("SELECT * FROM authorities WHERE id_administrator = ".$admin);

	
	
	if(($sql->rowCount())>0){
		$result = $sql->fetch(PDO::FETCH_ASSOC);
		if($result['id_subject'] == 0)
		{
		    $sql = $db->query("SELECT * FROM subjects");
		    while($rez = $sql->fetch(PDO::FETCH_ASSOC))
		    {
		        echo $rez['id']."\n";
		    }
		}
		else
		{
		    echo $result['id_subject']."\n";
	    }
	}
	

?>