
<!-- --------------------------------- -->
 <!-- [ Dashies Softwaries (c) 2020 ] -->
<!-- --------------------------------- -->

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <?php require "pony/hat.php"; ?>

    <style>
	.software-body {
	background-color: rgba(18, 0, 33, 1);
	max-width: 100%;
	min-width: 100%;
	width: 100%;
	max-height: 100%;
	min-height: 100%;
	height: 100%;
	position: absolute;
	}

    </style>
</head>

<?php require "pony/breast.php" ?>

<body>
    <div class="software-body">
	<div class="container downcol">
            <div class="row">
                <?php
                    $dbQ1 = $db->prepare('SELECT * FROM downloads ORDER BY id DESC');
                    $dbQ2 = $db->prepare('SELECT * FROM categories ORDER BY id');
                    $dbQ2->execute();
                    while ($row = $dbQ2->fetch(PDO::FETCH_LAZY))
                    {
                        $name = $row->NAME;
                        ?>
                        <div id="<?php echo $row->id ?>" style="display:inline_block" onclick="sort(this.id)" id="<?php echo $cat ?>"><?php echo $cat ?></div>
                        <?php
                    }
                ?>
            </div>
        </div>
	    
	<div>
	    <?php
		$dbQ1->execute();
		while ($row = $dbQ2->fetch(PDO::FETCH_LAZY))
		{
		    $name = $row->NAME;
		    $desc = $row->DESC;
		    $link = $row->LINK;
		    $img = $row->IMG;
	    ?>
		    <div class="container article">
			<h1><?php echo $name; ?></h1>
			<p><?php echo $desc; ?></p>
			<img src="<?php echo $img; ?>">
			<a href="<?php echo $link; ?>"></a>
		    </div>
	    <?php
		}
	</div>
    </div>

    <footer>
	<script src="js/sorter.js"></script>
	<?php require "pony/socks.php"; ?>
    </footer>
</body>
</html>
