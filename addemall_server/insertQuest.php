<?php

	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	
	$problem = $_POST["problem"];
	$level = $_POST["level"];
	$strenght = $_POST["strenght"];
	$author = $_POST["id_author"];
	$year = $_POST["id_year"];
	$subject = $_POST["id_subject"]; 
	$answer = $_POST["answer"];
	$order = $_POST["order"];
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	$sql = "SELECT id FROM question WHERE problem = '".$problem."'";
	$r = $db->query($sql);
	if($r->fetch(PDO::FETCH_ASSOC))
	{
	    echo "Postoji već zadatak";
	    exit(1);
	}
	if($order != 0)
	{
	    $sql = "SELECT id FROM level WHERE name = '".$level."'";
	    $r = $db->query($sql);
	    if($r->fetch(PDO::FETCH_ASSOC)){
	        echo "level već postoji";
	        exit(1);
	    }
	
	    $sql = "INSERT INTO level (name, id_year, id_subject, order) VALUES ('".$level."' , ".$year.", ".$subject.", ".$order.")";
	    $db->query($sql);
	    $sql = "SELECT id FROM level WHERE name = '".$level."'";
	    $r = $db->query($sql);
	    if($result = $r->fetch(PDO::FETCH_ASSOC))
	    {
	        $level = $result['id'];
	    }
	    
	}
	
	$sql = ("INSERT INTO question (problem, id_level, strenght, autor_id) VALUES ('".$problem."', ".$level.", ".$strenght.", ".$author.")");
	
    $db->query($sql);
    
    $sql = "SELECT id FROM question WHERE problem = '".$problem."'";
    $r = $db->query($sql);
    $result = $r->fetch(PDO::FETCH_ASSOC);
    $id = $result['id'];
    
    
    $sql1 = "SELECT id FROM answer WHERE result = '".$answer."'";
    $r = $db->query($sql1);
    if($result = $r->fetch(PDO::FETCH_ASSOC))
    {
        $answer = $result['id'];
    }
    else
    {
        $sql = "INSERT INTO answer (result) VALUES ('".$answer."')";
        $db->query($sql);
        $r = $db->query($sql1);
        if($result = $r->fetch(PDO::FETCH_ASSOC))
        {
            $answer = $result['id'];
        }
    }
    
    $sql = "INSERT INTO link (id_question, id_answer, correct) VALUES (".$id.", ".$answer.", true)";
    
    $db->query($sql);
    
    echo $id;
	
?>