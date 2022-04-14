<?php

	$server_username = "id3831008_admin";
	$server_password = "zaProjekt205";
	$dbName = "id3831008_addemall";
	
	
	$year = $_POST['year'];
	$level = $_POST['level'];
	$strenght = $_POST['strenght']; 
	$subject = $_POST['subject'];
	$author_id = $_POST['author_id'];
	$db = new PDO('mysql:host=localhost;dbname=id3831008_addemall', $server_username, $server_password);
	if(!$db){
		die("Connection Failed");
	}
	
	
	$sql = "SELECT * FROM question";
	$x = 1;
	
	if($year != "")
	{
	   $sql = $sql . " WHERE year = ".$year;
	   $x = 0;
	}
	if($level != "")
	{
	    if($x == 1)
	    {
	        $sql = $sql. " WHERE"; 
	        $x = 0;
	    }
	    else
	    {
	        $sql = $sql. " AND ";
	    }
	    $sql = $sql." level = ".$level;
	    
	}
	if($strenght != "")
	{
	    if($x == 1)
	    {
	        $sql = $sql. " WHERE" ;
	        $x = 0;
	    }
	    else
	    {
	        $sql = $sql. " AND ";
	    }
	    $sql = $sql." strenght = ".$strenght;
	    
	}
	if($subject != "")
	{
	    if($x == 1)
	    {
	        $sql = $sql. " WHERE";
	        $x = 0;
	    }
	    else
	    {
	        $sql = $sql. " AND ";
	    }
	    $sql = $sql." subject = ".$subject;
	    
	}
	if($author_id != 0)
	{
	    if($x == 1)
	    {
	        $sql = $sql. " WHERE";
	        $x = 0;
	    }
	    else
	    {
	        $sql = $sql. " AND ";
	    }
	    $sql = $sql." author_id = ".$author_id;
	    
	}
	
	$stmt = $db->query($sql);
	echo $sql;
	
	while($result = $stmt->fetch(PDO::FETCH_ASSOC))
		{
		    echo $result['id']."|".$result['problem']."|".$result['level']."|".$result['year']."|".$result['strenght']."|".$result['autor_id']."|".$result['subject']."\n";
		}
	
	
	if(!$stmt){
	    echo $sql;
	    echo $db->error;
	}
	

?>