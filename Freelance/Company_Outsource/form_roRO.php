<?php
    $name = $_POST['name'];
    $phoneOrMail = $_POST['mail telephone'];

    $name = htmlspecialchars($fio);
    $phoneOrMail = htmlspecialchars($email);

    $name = urldecode($fio);
    $phoneOrMail = urldecode($email);

    $name = trim($fio);
    $phoneOrMail = trim($email);

    if (mail("ighosta9@gmail.com", "Заявка с сайта.", "Имя: ".$name.". Почта или телефон пользователя: ".$phoneOrMail ,"From: websitemail@domain.com \r\n")) {  
        header('Refresh: 0; URL=http://test1.ru/index_roRO.html');
    } else { 
        echo "<script type='text/javascript'>alert('Form success sended!');</script>";
    }
?>