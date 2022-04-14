<?php

	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	$admin =  $_POST["admin_id"];
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	
	
	$sql = "SELECT `problem`, result, sum(case when results.answered = answer.result then 1 ELSE 0 end) as numberOfcorrect, count(*) as numberOfAnswered FROM `question` JOIN link ON `id` = link.id_question JOIN answer ON link.id_answer = answer.id JOIN results ON question.id = results.id_question";
	
	
	if($admin != 0)
	{
	    $sql=$sql." WHERE question.autor_id = ".$admin; 
	}
	
	$sql = $sql." GROUP by question.id";
	
	$stmt = $db->query($sql);
	while($result = $stmt->fetch(PDO::FETCH_ASSOC))
		{
		    echo $result['problem']."|".$result['result']."|".$result['numberOfcorrect']."|".$result['numberOfAnswered']."\n";
		}
	
	
	if(!$stmt){
	    echo $sql;
	    echo $db->error;
	}
	

?>