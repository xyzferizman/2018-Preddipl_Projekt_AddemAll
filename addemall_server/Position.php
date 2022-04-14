<?php

	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	
	$id_user = $_POST["user"];
	$subject = $_POST["subject"];
	$strenght = $_POST["strenght"];
	$order = $_POST["order"];
	$expirience = $_POST["expirience"];
	
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	
	
	$sql = "SELECT * FROM postion_user, users WHERE id = id_player AND id_player = ".$id_user;
	
	$stmt = $db->query($sql);
	$resultString = "";
	while($result = $stmt->fetch(PDO::FETCH_ASSOC))
		{
		    if($subject == $result['id_subject'])
		    {
		        if($expirience>$result['expirience'])
		        {
		            $sql = "UPDATE users SET expirience = ".$expirience." WHERE id = ".$id_user;
		            $db->query($sql);
		            $result['expirience']=$expirience;
		        }
		        if($order>=$result['order'])
		        {
		            $sql = "UPDATE position_user SET order = ".$order.", strenght = ".$strenght." WHERE id_player = ".$id_user." AND id_subject = ".$subject;
		            $db->query($sql);
		            $result['order']=$order;
		            $result['strenght'] =$strenght;
		        }
		        else if($strenght>=$result['strenght'])
		        {
		            $sql = "UPDATE position_user SET strenght = ".$strenght." WHERE id_player = ".$id_user." AND id_subject = ".$subject;
		            $db->query($sql);
		            $result['strenght']=$strenght;
		        }
		        
		    }
		    $resultString=$resultString.$result['id_user']."|".$result['id_subject']."|".$result['strenght']."|".$result['order']."|".$result['expirience']."\n";
		}
	
	echo $resultString;
	
	if(!$stmt){
	    echo $sql;
	    echo $db->error;
	}
	

?>