<?php

	$servername = "addemall.000webhostapp.com";
	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	
	$email = $_POST["email"];
	$password =$_POST["password"];
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	$sql = $db->prepare("SELECT * FROM users LEFT JOIN users_state ON users.id = users_state.id_user  WHERE users.email =?");
	$sql->execute(array($email));
	
	
	if(($sql->rowCount())>0){
		$result = $sql->fetch(PDO::FETCH_ASSOC);
		if($result['password'] == $password)
		{
		    echo $result['id']."|".$result['Name']."|".$result['Surname']."|".$result['email']."|".$result['year']."|".$result['health']."|".$result['level']."|".$result['admin'];
		}
		else echo "kriva lozinka";
	}
	else echo "ne postoji korisnik";
	

?>