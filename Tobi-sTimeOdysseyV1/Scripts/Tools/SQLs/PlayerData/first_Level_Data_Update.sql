-- Create all Level 


-- Create all Level from the Tutorial 1 

INSERT INTO Player_Level_Data (ID, lvType, lvGroup, lvNumber, locked) 
VALUES	(1, 'Tutorial', 1, 1, 'N'),
		(2, 'Tutorial', 1, 2, 'Y'),
		(3, 'Tutorial', 1, 3, 'Y');


-- Create all Level from the Mission 1 

INSERT INTO Player_Level_Data (ID, lvType, lvGroup, lvNumber, locked) 
VALUES	(4, 'Mission', 1, 1, 'Y'),
		(5, 'Mission', 1, 2, 'Y'),
		(6, 'Mission', 1, 3, 'Y'),
		(7, 'Mission', 1, 4, 'Y'),
		(8, 'Mission', 1, 5, 'Y');


-- Create all Level from the Tutorial 2 

INSERT INTO Player_Level_Data (ID, lvType, lvGroup, lvNumber, locked) 
VALUES	(9, 'Tutorial', 2, 1, 'Y');


-- Create all Level from the Mission 2

INSERT INTO Player_Level_Data (ID, lvType, lvGroup, lvNumber, locked) 
VALUES	(10, 'Mission', 2, 1, 'Y'),
		(11, 'Mission', 2, 2, 'Y');