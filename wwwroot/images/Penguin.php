<?php
/*
 * use the
 * 
 *  parent::
 *  
 * resolution operator to access parent functions 
 * or properties
 * */
require_once("Bird.php");

class Penguin extends Bird{
	/*
	 * if Penguin re-defines (overrides)
	 * the parent Bird constructor, explictly invoke it
	 * in order to pass along the parameter values
	 * 
	 *  if Penguin does not re-define the constructor,
	 *  this is not required
	 * */
	public function __construct($eyeColor, $song){
		/*
		 * explicitly call the parent constructor to ensure it is run
		* */
		parent::__construct($eyeColor, $song);
		echo "<p>A Penguin is born!</p>";
	}
	/*
	 * add to the parent's functions
	 * */
	public function swim(){
		echo "<p>Swim swim...</p>";
	} 
	/*
	 * override the parents functions...
	 * */
	public function fly(){
		echo "<p>Penguins don't fly. Good luck trying...</p>";

	}

}

?>