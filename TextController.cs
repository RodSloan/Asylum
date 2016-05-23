using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
* Game by Rodney Sloan
* Based off a Unity tutorial on Udemy
* License: 
*	This work is licensed under a Creative Commons Attribution-ShareAlike 4.0 International License.
*	Find out more at http://creativecommons.org/licenses/by-sa/4.0/
**/

public class TextController : MonoBehaviour {
	
	//TODO: rooms would make great classes, then we could inherit things like movement and help
	
	public Text text;
	private enum States {
		cell_start, cell, corridor1, corridor2, corridor3, corridor4, doorlock, cockpit, 
		tag, cipherDoor, cipher, 
		ghostCell1, ghostCell2, empty, help, credits 
	};
	private States myState;
	private States SaveState;
	
	// Flags, prefix flg_
	private bool flg_ghosting = true, flg_first_unghosting = false;
	private bool flg_cell_visited = false;
	private bool flg_corr1_visited = false;
	private bool flg_corr2_visited = false;
	//private bool flg_corr3_visited = false;
	private bool flg_corr4_visited = false;
	private bool flg_doorlock_visited = false;
	private bool flg_ghostCell1_visited = false;
	private bool flg_ghostCell2_visited = false;
	private bool flg_TAG_visited = false;
	private bool flg_Cipher_visited = false;
	private bool flg_Cipher_recruited = false;
	private bool flg_lock_plans = false; 
	private bool flg_Solar_recruited = false; //Solar is an NPC, recruit him to blow the door
	private bool flg_extinguisher = false;
	private bool flg_trueLove = false; 	//Set when you discover TAGs love for Cipher
	
	// Use this for initialization
	void Start () {
		myState = States.cell_start;
		
	}
	
	// Update is called once per frame
	void Update () {
		print (myState);
		switch(myState) {
		case States.cell_start: 	cell_start();		break;
		case States.cell: 			cell();		 	break;
		case States.corridor1: 		corridor1();		break;
		case States.corridor2:		corridor2();		break;
		case States.corridor3:		corridor3();		break;
		case States.corridor4:		corridor4();		break;
		case States.doorlock:		doorlock();		break;
		case States.cockpit:		cockpit();		break;
		case States.tag:			TAG();			break;
		case States.cipherDoor:		cipherDoor();		break;
		case States.cipher:			cipher();		break;
		case States.empty:			empty();		break;
		case States.ghostCell1:		ghostCell1();		break;
		case States.ghostCell2:		ghostCell2();		break;
		case States.help:			help();			break;
		case States.credits:		credits();		break;
		default:
			text.text = "ERROR: Room does not exist.";
			break;
		}
		
		/*if (myState == States.cell_start) {
			;
		} else if (myState == States.corridor1) {
			corridor1();
		}*/
	}
	
	void cell_start () {
		text.text = 
			"The agony is intense — a cold white stab lancing your brain." + 
				"\n\tWhen you regain your senses, you find yourself standing in a dark, " +
				"cramped space. A cell? " +
				"\n\tTo your left, barely visible in the gloom, you see a large restraining chair, " +
				"lying on its side. Did you do that? Still reeling from the pain, you try to collect your " +
				"thoughts, but try as you might you can’t remember how you got here. " +
				"\n\tA door to the WEST stands open, leading out into more darkness beyond." + 
				"\n\nUse the ASWD/Arrow Keys to move." +
				"\nPress A to head West, out the door." +
				"\n\nPress H for Help.";
		
		if(key_controller() == 1) {
			myState = States.corridor1;
		} else if(key_controller() == 6) {
			SaveState = States.cell_start;
			myState = States.help;
		}
	}
	
	void cell () {
		/* 	A little tricky:
		*	flg_cell_visited triggers when you first step back into the cell
		*	flg_first_unghosting triggers when you touch her arm
		*/
		if ((!flg_cell_visited) && (flg_ghosting)) { 
			text.text = "As you step back into the cell, you freeze." +
				"\n\tShrouded in roiling blue flames, a woman stands before you, her eyes " +
					"closed and her head forward, as if in prayer. She doesn’t seem to notice the flames, " +
					"much less your arrival." +
					"\n\tShe has long, dark hair and wears a white uniform with black indication " +
					"markings. A name badge over her left breast reads ‘Inmate 13’, with the words " +
					"‘Projection Lock’ written beneath." +
					"\nPress “SPACE” to reach out and touch her arm.";
			//Change pic of woman
		} else if((!flg_first_unghosting)/* && (!ghosting)*/) {
			text.text = "An invisible force spins you around suddenly, and instantly your senses explode. The " +
				"smell of ozone. The frigid cold. An aching hunger in your belly. How did you not " +
					"notice any of those things before?" +
					"\n\tYou turn to the woman, frantically wondering what hit you, your hair " +
					"whipping as you whirl, but she is gone." +
					"\n\tThen you see her — staring back in shock — a dim image reflected in a mirror." +
					"\nThe door WEST remains open.";
		} else if (flg_ghosting) {
			text.text = "Your body stands before you, radiating blue flames." +
				"\nPress SPACE to resume normal form.";
		} else if (!flg_ghosting) {
			text.text = "The cell is empty, but safe; a better place than any to leave your body." +
				"\nPress SPACE to resume your ghost form.";
		}
		
		if(key_controller() == 1) {
			// 
			if(flg_cell_visited) { 
				flg_first_unghosting = true;
				if(flg_ghosting)
					flg_ghosting = false;
				else
					flg_ghosting = true;
			}
			myState = States.corridor1;
		} else if(key_controller() == 5) {
			flg_cell_visited = true;
			myState = States.cell;
		} else if(key_controller() == 6) {
			SaveState = States.cell;
			myState = States.help;
		}
	}
	
	void corridor1 () {
		if (!flg_corr1_visited && flg_ghosting) {
			text.text = "You step out into a corridor lit by faint, ghostly luminescent emergency strips, which " +
				"disappear off into the darkness to the NORTH. To the EAST is the room from which " +
					"you came, while a heavy security door bars the way WEST. Near you, the southern, " +
					"rusted wall has a dead data screen and ‘Cell Block 6’ stenciled in faded white " +
					"lettering upon it. ";
		} else if (flg_corr1_visited) {
			text.text = "You are at the southern end of a corridor, lit by ghostly luminescent emergency " +
				"strips that disappear off into the darkness to the NORTH. To the EAST is the room " +
					"you started in, while a heavy security door stands to the WEST. Near you, the " +
					"southern wall has ‘Cell Block 6’ stenciled on its rusted metal surface, next to a dead " +
					"data screen. ";
		}
		
		if(key_controller() == 1) {
			//myState = States.corridor1;
		} else if(key_controller() == 2) {
			flg_corr1_visited = true; //Once we visit it is always true, no need to check.
			myState = States.corridor2;
		} else if(key_controller() == 4) {
			flg_corr1_visited = false; //Once we visit it is always true.
			myState = States.cell; //This changes to turn human
		} else if(key_controller() == 6) {
			SaveState = States.corridor1;
			myState = States.help;
		}
	}
	
	void corridor2 () {
		if (!flg_corr2_visited) {
			text.text = "The corridor is eerily quiet, save for the long, drawn out screech of metal on metal, " +
				"somewhere far below you. " +
					"\n\tIn the darkness you can barely make out small details. Rust covered panels, " +
					"their security bolts juddered loose. A hanging power jack that someone should have " +
					"fixed ages ago. Shattered plasglass from an overhead lumo strip. These are not the " +
					"marks of war, but the signs of the passing of time. The passing of years. " +
					"\n\tThe corridor continues NORTH into blackness. To the WEST and EAST are " +
					"more security doors, one to either side.";
		} else if (flg_corr2_visited) {
			text.text = "The corridor continues NORTH and SOUTH into darkness. To the WEST and EAST are " +
				"more security doors, one to either side.";
		}
		
		if(key_controller() == 1) {
			//myState = States.cell_start;
		} else if(key_controller() == 2) {
			myState = States.corridor3;
		} else if(key_controller() == 3) {
			myState = States.corridor1;
		} else if(key_controller() == 4) {
			//myState = States.corridor1;
		} else if(key_controller() == 6) {
			SaveState = States.corridor2;
			myState = States.help;
		}
	}
	
	void corridor3 () {
		if ((!flg_ghosting) && (!flg_extinguisher)) {
			text.text = "A dull emergency beacon flashes feebly, running on the last of its power. The sickly " +
				"orange glow illuminates a thick blast door to the EAST. To either side of the blast " +
					"door are two large, dry type FIRE EXTINGUISHERS. Another security door, reinforced " +
					"with titanium bars, stands to the WEST. " +
					"\nPress SPACE to take one of the EXTINGISHERS.";
		} else if ((flg_ghosting) && (!flg_extinguisher)) {
			text.text = "A dull emergency beacon flashes feebly, running on the last of its power. The feeble " +
				"gray glow illuminates a thick blast door to the EAST. To either side of the blast door " +
					"are two large, dry type FIRE EXTINGUISHERS. Another security door, reinforced with " +
					"titanium bars, stands to the WEST. ";
		} else if ((flg_ghosting) && (flg_extinguisher)) {
			text.text = "The gray emergency beacon flashes briefly, then dies, then flashes for a moment, " +
				"intermittently now. A thick blast door stands to the EAST. One of the fire " +
					"extinguisher bays is empty, since you removed an extinguisher earlier. A security " +
					"door, reinforced with titanium bars, stands to the WEST. ";
		} else if ((!flg_ghosting) && (flg_extinguisher)) {
			text.text = "The emergency beacon flashes briefly, then dies, then flashes for a moment, " +
				"intermittently now. A thick blast door stands to the EAST. One of the fire " +
					"extinguisher bays is empty, since you removed an extinguisher earlier. A security " +
					"door, reinforced with titanium bars, stands to the WEST.";
		}
		
		if(key_controller() == 1) {
			//myState = States.cell_start;
		} else if(key_controller() == 2) {
			myState = States.corridor4;
		} else if(key_controller() == 3) {
			myState = States.corridor2;
		} else if(key_controller() == 6) {
			SaveState = States.corridor3;
			myState = States.help;
		}
	}
	
	void corridor4 () {
		if ((!flg_corr4_visited) && (flg_ghosting)) { 
			text.text = "You feel it before you see it. Out of the gloom to the NORTH, a large airlock emerges, " +
				"blocking your way. The door seems to crackle with unseen static energy, even " +
					"though its console is completely dead." +
					"\n\tMore security doors line the corridor, one to the WEST and one to the EAST. " +
					"A soft white glow emanates from a view slit in the door to the WEST.";
		} else if ((!flg_corr4_visited) && (!flg_ghosting)) {
			text.text = "Out of the gloom to the NORTH, a large airlock emerges, blocking your way. " +
				"The console in its center is completely dead." +
					"\n\tMore security doors line the corridor, one to the WEST and one to the EAST. " +
					"A soft white glow emanates from a view slit in the door to the WEST.";
		}
		
		if(key_controller() == 1) {
			//myState = States.corridor1;
		} else if(key_controller() == 2) {
			myState = States.doorlock;
		} else if(key_controller() == 3) {
			myState = States.corridor3;
		} else if(key_controller() == 4) {
			//myState = States.corridor2;
		} else if(key_controller() == 6) {
			SaveState = States.corridor4;
			myState = States.help;
		}
	}
	
	void doorlock () {
		/*
		* TODO: Do we wrap the controls in if statements or put them in with the options allowed.
		*/
		if ((!flg_doorlock_visited) && (flg_ghosting)) { 
			text.text = "As you approach the door you’re slammed back, you vision blazing white in an " +
				"explosion of pain. " +
					"\n\tWhen your head clears you find yourself standing in the cell where you " +
					"began, head still swimming with pain. " +
					"\n\tYour only option is to head WEST, out of the cell.";
		} else if ((!flg_doorlock_visited) && (!flg_ghosting)) {
			text.text = "The door is locked, with no obvious way to open it from this side. The emergency " +
				"release is probably on the other side, and with a dead console the entire door is " +
					"probably unpowered, so even if you had a key… " +
					"\n\tThere is no way through.";
		} else if ((flg_doorlock_visited) && (flg_ghosting)) {
			text.text = "You approach the door cautiously, but the static energy emanating from it " +
				"grows more and more intense, making your head hurt and your vision blur. You " +
				"back away.";
		} else if ((flg_doorlock_visited) && (!flg_ghosting)) {
			text.text = "The door remains locked.";		
		} else if ((!flg_ghosting) && (flg_Solar_recruited)) {
			text.text = "As you near the airlock, Solar walks past you, stepping up to the door and presses " +
				"his palms against it. His hands begin to glow a soft red, then intensify to orange, " +
				"then yellow, then white, filling the corridor with intensely bright light." +
				"\n\tAs your eyes struggle to adjust, you see the metal of the airlock glowing red. " +
				"Melting, collapsing, burning. " +
				"\n\tThen the light dies and Solar steps back, his face lit by the glowing orange " +
				"slag pile at his feet as he turns to you." +
				"\n\t“Ladies first.”";
		} else if ((flg_ghosting) && (flg_Solar_recruited)) {
			text.text = "You try to get closer to the door, but whatever hit you before is still " +
				"cackling with unseen power. You dare not go any nearer.";
		}
		
		if(key_controller() == 2) {
			myState = States.cockpit;
		} else if(key_controller() == 3) {
			myState = States.corridor4;
		} else if(key_controller() == 6) {
			SaveState = States.doorlock;
			myState = States.help;
		}
	}
	
	void cockpit () {
		if (!flg_ghosting) { 
			text.text = "You step through the flaming slag and gaping hole that remains of the " +
				"airlock, and onto the ships command deck. Beyond the semicircle of derelict " +
				"navports and lifeless comm arrays, the ships view port is filled with the soft orange " +
				"glow of a nearby planet’s dust belt. " +
				"\n\tAs you step forward, you brush against a flight seat, disturbing some " +
				"unseen weight resting within the chair. You turn to stop the thing falling, coming " +
				"face to face with the death grin of a rotting corpse in a captain’s flight suit. " +
					"\n\tMost of the crew seats are empty, but here and there other corpses rest, " +
					"blanketed under a thick layer of dust. " +
					"\n\tHow long has the ship been adrift in space, and how much longer will it " +
					"float aimlessly still…" +
					"\n\nPress SPACE to continue";
		} else if(flg_ghosting) {
			text.text = "You step over the slag and through the gaping hole that remains of the airlock, and " +
				"onto the ships command deck. Beyond the semicircle of derelict navports and " +
					"lifeless comm arrays, the ships view port is filled with a soft gray glow from a nearby " +
					"planet’s dust belt. " +
					"\n\tGrey flames engulf several chairs, where the remains of long dead crew " +
					"members rot, still manning their posts. " +
					"\n\tHow long has the ship been adrift in space, and how much longer will it " +
					"float aimlessly still…" +
					"\n\nPress SPACE to continue";
		}
		
		if(key_controller() == 5) {
			myState = States.credits; //change to see credits
		} else if(key_controller() == 6) {
			SaveState = States.doorlock;
			myState = States.help;
		}
	}
	
	void TAG () {
		if ((!flg_TAG_visited) && (flg_ghosting)) {
			text.text = "Passing through the still blackness, you suddenly see gray, licking flames, roil across " +
				"the body of a dead woman, reclining in a chair in front of a small writing desk. The " +
					"flames don’t seem to affect the corpse or her chair, or shed light or heat. ";
		} else if ((flg_TAG_visited) && (flg_ghosting)) {
			text.text = "Within the cell, a dead woman is swathed in roiling grey flames. It appears she died " +
				"while writing at her desk, and in the dim light of an emergency lumo strip you can " +
					"just make out some of the words." +
					"\n\n\t‘My Genius."+
					"\n\tEven though we are apart now, locked only a few feet from each " +
					"other in this iron tomb, I still remember your face, your touch, your endless, " +
					"boundless love. " +
					"\n\tYou taught me what love meant, and now, with nothing but my " +
					"memories, I understand, and can finally say, with true confidence, that I " +
					"will always love you.’" +
					"\n\tYours, Taggie";
			flg_trueLove = true; // Yay!!!	
		} else if (!flg_ghosting) {
			text.text = "The security door is locked, and the room beyond is too dark to see " +
				"anything through the view port.";
		} 
		
		if(key_controller() == 6) {
			SaveState = States.tag;
			myState = States.help;
		}
	}
	
	void cipherDoor() {
		if (!flg_Cipher_visited && flg_ghosting) {
			text.text = "Looking through the viewport, you see violent blue flames twisting around an old " +
				"man sitting on the edge of his cot, apparently deep in thought despite the " +
					"conflagration. Surrounding him and stacked up against the walls are piles of thick " +
					"books. At the foot of his desk, also covered with books, is a small, rotating " +
					"contraption. A cable runs from the device up into the ceiling, where it connects " +
					"with the cell’s lumostrips." +
					"\n\tPress SPACE to push on the door.";
		} else if (!flg_Cipher_visited && !flg_ghosting) {
			text.text = "Looking through the viewport, you see an old man sitting on the edge of his cot, " +
				"apparently deep in thought. Surrounding him and stacked up against the walls are " +
					"piles of thick books. At the foot of his desk, also covered with books, is a small, " +
					"rotating contraption. A cable runs from the device up into the ceiling, where it " +
					"connects with the cells lumostrips." +
					"\n\tPress SPACE to try banging on the door.";
		} else if (flg_Cipher_visited && flg_ghosting) {
			text.text = "Looking through the view port you see an old man, bathed in blue flames, deep in " +
				"thought. " +
					"\n\tPress SPACE to push on the door.";
		} else if (flg_Cipher_visited && !flg_ghosting) {
			text.text = "Looking through the view port you see an old man, sitting on his cot, deep in " +
				"thought. " +
					"\n\tPress SPACE to try banging on the door.";
		}
		
		//TODO: wrap in logic
		if(key_controller() == 5) {
			
		} else if(key_controller() == 6) {
			SaveState = States.cipherDoor;
			myState = States.help;
		}
	}
	
	void cipher() {
		//This layout makes provision for imaging
		if (flg_ghosting && !flg_Cipher_recruited) {
			if (!flg_Cipher_visited) {
				text.text = "You heave against the door, but, surprisingly, your hands pass right through, and " +
					"with the momentum of you push, the rest of you follows, into the white lit room." +
						"\n\tThe man doesn’t seem to notice your entrance, and even talking to him " +
						"doesn’t stir him from his thoughts. You try tapping him on the shoulder, your hand " +
						"passing unharmed through the halo of blue flame, but that also doesn’t work. " +
						"\n\tEventually, you give up and look around the room. The books cover many " +
						"subjects, from nuclear physics to medicine, to politics and deep-space flight systems." +
						"Besides the books on the desk, there are a number of designs, including an " +
						"exploded view of the security door’s locking mechanism. Turning to look at the door, " +
						"you see that the lock appears to have been polished. A number of handmade tools " +
						"rest atop a stack of books near the door." +
						"\t\nWith no way to rouse the man, there’s nothing else to do but to head EAST, " +
						"back into the corridor.";
				
				flg_lock_plans = true;
			} else if (flg_Cipher_visited) {
				text.text = "";
			}
		} else if (!flg_ghosting && !flg_Cipher_recruited) {
			if (!flg_Cipher_visited) {
				text.text = "You bang loudly on the door, and the man jumps with a start. He peers at you, as if " +
					"you are the most alien being in the world, while slowly drawing closer to the " +
						"viewport." +
						"\n\t‘It can’t be.’ He says, ‘Ha! You’re alive. A ghost returned! Fitting, really. It IS " +
						"so good to see you again.”" +
						"\n\tYou tell the man — who calls himself Cipher — that you can help him " +
						"escape, but he only shakes his head sadly." +
						"\n\t‘No, the things they’d do if I left this cell. No. I made a promise.’ " +
						"\n\tYou try to convince Cipher, but he’s unreasonably obstinate. " +
						"\n\tFrustrated, you decide to head back into the corridor to the EAST to clear your head.";
			} else if (flg_Cipher_visited) {
				text.text = "You knock on the door again and the man approaches the viewport. "; 
			} else if (flg_lock_plans && flg_trueLove) {
				text.text = "You knock on the door and Cipher answers." +
					"\n\t‘Ghost, is there something I can do for you?’ " +
						"\n\t‘Those plans in your room, I want you to open that lock Cipher!’" +
						"\n\t‘Did you forget? I made a promise. I’m not going anywhere…’" +
						"\n\t‘Because of Taggy?’" +
						"\n\t‘That… yes.’" +
						"\n\t‘She loved you, you know. The last thing she wrote about was her love for you, she " +
						"put it down in a letter to you.’" +
						"\n\tThe revelation brings a sea change on the man, who looks into your eyes, searching " +
						"to know the truth of what you say. Then he disappears behind the door. Not two " +
						"seconds later the cell door slides open, and Cipher stands before you.";
			} else if (flg_lock_plans && flg_Cipher_visited) { //assuming you have to visit him before
				text.text = "You knock on the door and Cipher answers." +
					"‘Ghost, how can I help you?’ " +
						"‘What about those plans in your room. You know how to get through that lock!’" +
						"‘Yes, but I already told you, I’m not leaving.’";
			} 
		} else if (flg_Cipher_recruited) {
			text.text = "Cipher stands before you, his room open." +
				"‘Just let me know if I can help?’";
		}
		
		if(key_controller() == 6) {
			SaveState = States.cipher;
			myState = States.help;
		}
	}
	
	void ghostCell1 () {
		if (!flg_ghostCell1_visited && flg_ghosting) {
			text.text = "The security door is locked, but through a narrow view port you see a lifeless figure " +
				"bathed in ghostly grey flames, lying on a cot. The figure seems unaffected by the " +
					"flames, which cast no light.";	
		} else if (!flg_ghostCell1_visited && !flg_ghosting) {
			text.text = "The security door is locked, but through a narrow view port you see a lifeless figure " +
				"lying on a cot.";
		} else if (flg_ghostCell1_visited && flg_ghosting) {
			text.text = "Through the view slit of the locked security door you see a lifeless figure bathed in " +
				"gray flames and lying on a cot.";
		} else if (flg_ghostCell1_visited && !flg_ghosting) {
			text.text = "Through the view slit of the locked security door you see a lifeless figure lying on a " +
				"cot.";
		}
		
		if(key_controller() == 6) {
			SaveState = States.ghostCell1;
			myState = States.help;
		}
	}
	
	void ghostCell2 () {
		if (!flg_ghostCell2_visited && flg_ghosting) {
			text.text = "The security door is locked, but through a narrow viewport you see a lifeless figure " +
				"bathed in ghostly grey flames, lying in a heap on the floor. The figure seems " +
					"unaffected by the flames, which cast no light.";	
		} else if (!flg_ghostCell2_visited && !flg_ghosting) {
			text.text = "The security door is locked, but through a narrow viewport you can barely " +
				"see a lifeless figure lying in a heap on the floor.";
		} else if (flg_ghostCell2_visited && flg_ghosting) {
			text.text = "Through the view slit of the locked security door you see a lifeless figure bathed in " +
				"grey flames, lying in a heap on the floor.";
		} else if (flg_ghostCell2_visited && !flg_ghosting) {
			text.text = "Through the view slit of the locked security door you see a lifeless figure lying in a " +
				"heap on the floor.";
		}
		
		if(key_controller() == 6) {
			SaveState = States.ghostCell2;
			myState = States.help;
		}
	}
	
	void empty() {
		text.text = "You walk through the open security door into an unlit, hollow cell. Your " +
			"only option is to go back out through the door to the EAST.";
		
		if(key_controller() == 6) {
			SaveState = States.empty;
			myState = States.help;
		}
	}
	
	void help () {
		text.text = "+++ How To Play +++" +
			"\n\nA or Right Arrow: Move West." +
				"\nW or Up Arrow: Move North." +
				"\nS or Down Arrow: Move South." +
				"\nD or Right Arrow: Move East." +
				"\nSPACE Key: Interact with an object or character." +
				"\nH: This help menu." +
				"\n\nPress H to return.";
		if(key_controller() == 6)
			myState = SaveState;
	}
	
	void credits () {
		text.text = "CREDITS" +
			"\n\nGAME\t\t\t\trodney sloan" +
				"\n\nMUSIC\t\t\t\tarctic caves" +
				"\n\t\t\t\t\t\ttechnoaxe" +
				"\n\nPUBLISHER\t\trising phoenix games" +
				"\n\t\t\t\t\t\twww.risingphoenixgames.com" +
				"\n\nThanks for playing..." +
				"\n\nPress SPACE to replay.";
		if(key_controller() == 5) {
			myState = States.cell_start; //change to see credits
		} else if(key_controller() == 6) {
			SaveState = States.credits;
			myState = States.help;
		}
	}
	
	/* Maps integers to common keys
	 * Returns int
	 * TODO: map in buttons on a game pad
	 */	
	int key_controller () {
		
		if((Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.LeftArrow))) {
			return 1;
		} else if((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow))) {
			return 2;
		} else if((Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.DownArrow))) {
			return 3;
		} else if((Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.RightArrow))) {
			return 4;
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			return 5;
		} else if (Input.GetKeyDown(KeyCode.H)) { //H key for Help
			return 6;
		} 
		else 
			return 0;
	}
}
