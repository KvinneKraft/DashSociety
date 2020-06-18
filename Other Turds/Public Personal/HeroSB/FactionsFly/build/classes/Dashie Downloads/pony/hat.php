
<style>
    .top-main-container {
    min-width: 600px;
    max-width: 100%;
    width: 100%;
    min-height: 64px;
    max-height: 64px;
    height: 64px;
    display: inline-block;
    margin: 0 auto;
    background-color: rgba(25, 0, 41);
    z-index: 100;
    }
    .top-container {
    min-width: 800px;
    max-width: 800px;
    width: 100%;
    min-height: 100%;
    max-height: 100%;
    height: 100%;
    display: table;
    margin: 0 auto;
    background-color: inherit;
    }
    .top-container-title {
    font-size: 34px;
    text-shadow: 0 0 30px #FF0000, 0 0 32px #0000FF;
    color: white;
    position: relative;
    float: left;
    top: 10px;
    }
    .top-container-menu {
    float: right;
    }
    .top-container-menu button {
    font-family: 'dash';
    font-size: 26px;
    text-shadow: 0 0 16px #FF0000, 0 0 16px #0000FF;
    color: white;
    height: 54px; 
    width: 135px;
    position: relative;
    top: 5px;
    border-radius: 8px;
    background-color: inherit;
    }
    .top-container-menu button:hover {
    background-color: rgba(37, 0, 61);
    }
    .top-container-menu button:focus {
    background-color: rgba(45, 0, 74);
    outline: none !important;
    }
</style>

<html>
    <head>
	<title> Kvinne Kraft - www.pugpawz.com </title>
	<meta charset="utf-8" />

	<link href="https://cdn.jsdelivr.net/npm/fork-awesome@1.1.7/css/fork-awesome.min.css" integrity="sha256-gsmEoJAws/Kd3CjuOQzLie5Q3yshhvmo7YNtBG7aaEY=" crossorigin="anonymous" rel="stylesheet">
	<link href="pony/css/image/favicon.ico" rel="shortcut icon" />
	<link href="pony/css/global.css" rel="stylesheet" />

	<div class="top-main-container">
	    <div class="top-container">
		<div class="top-container-title">
		    <a href="https://pugpawz.com/">
			Dashies Fluffy Nest
			<img src="pony/css/image/logo.png" width="56" height="32" style="position:relative; top: 2px" />
		    </a>
		</div>
		<script language="javascript">
		    function redirect(id)
		    {
			var uri = "index.php";

			switch ( id )
			{
			    case 2:
			    {
				uri = "softwaries.php";
				break;
			    };

			    case 3:
			    {
				uri = "dashkraft.php";
				break;
			    };
			};

			if (uri != location.href.split("/").slice(-1))
			{
			    window.location = uri;
			};
		    };
		</script>
		<div class="top-container-menu">
		    <button onclick="redirect(1)"> Home </button>
		    <button onclick="redirect(2)"> Softwaries </button>
		    <button onclick="redirect(3)"> Dash Kraft </button>
		</div>
	    </div>
	</div>
    </head>
</html>