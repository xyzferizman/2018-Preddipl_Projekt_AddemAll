<?php

	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	$player = $_POST["user_id"];
	$admin = $_POST["admin_id"];
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	
	
	$sql = "SELECT player.id as playerId, player.Name as playerName, player.Surname as playerSurname, player.email as playerEmail, question.problem, level.name as levelName, year.name as yearName, strenght, autor_id, admin.Name as adminName, admin.surname as adminSurname, admin.email as adminEmail, answered, answer.result, results.time, level.id as idLevel FROM results, users as player, question, link, answer, level, users as admin, year, subjects WHERE results.id_player = player.id AND results.id_question = question.id AND question.autor_id = admin.id AND results.id_question = link.id_question AND link.id_answer = answer.id AND question.id_level = level.id AND level.id_subject = subjects.id AND subjects.id_year = year.id AND link.correct = true";
	
	if($player != 0)
	{
	    $sql=$sql." AND results.id_player = ".$player;
	}
	if($admin != 0)
	{
	    $sql=$sql." AND question.id_autor = ".$admin; 
	}
	$stmt = $db->query($sql);
	while($result = $stmt->fetch(PDO::FETCH_ASSOC))
		{
		    echo $result['playerId']."|".$result['playerName']."|".$result['playerSurname']."|".$result['playerEmail']."|".$result['problem']."|".$result['levelName']."|".$result['yearName']."|".$result['strenght']."|".$result['autor_id']."|".$result['adminName']."|".$result['adminSurname']."|".$result['adminEmail']."|".$result['answered']."|".$result['result']."|".$result['time']."|".$result['idLevel']."\n";
		}
	
	
	if(!$stmt){
	    echo $sql;
	    echo $db->error;
	}
	

?>