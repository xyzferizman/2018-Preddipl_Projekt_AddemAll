<?php

	$servername = "addemall.000webhostapp.com";
	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	$year = 0; //$_POST["id_year"];
	$subject = 0;//$_POST["id_subject"];
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	
	$sql = "SELECT * FROM level";
	$x= 0;
	if($year != 0)
	{
	    $sql=$sql." WHERE id_year =".$year;
	    $x=1;
	}
	if($subject != 0)
	{
	    if($x == 0)
	    {
	        $sql = $sql. " WHERE";
	    }
	    else
	    {
	        $sql = $sql. " AND";
	    }
	    $sql= $sql." id_subject =".$subject;
	    $x = 1;
	        
	}
	
	$r =$db->query($sql);
	

	
	
	while($result = $r->fetch(PDO::FETCH_ASSOC)){
	    if($x != 0)
	    {
	        echo $result['id']."-".$result['name']."\n";
	    }
	    else
	    {
	        echo $result['id']."|".$result['name']."|".$result['id_subject']."|".$result['order']."\n";
	    }
	}
	

?>