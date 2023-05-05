-- Fill in all Tutorial 1.1 Data inside the Level Tables


-- Fill in the data for Tutorial 1.1

INSERT INTO Level_Data (ID, Era, Place, PxPos, PyPos) VALUES(1, 'Edo', 'Dojo', 2, 3);


-- Fill in the map for Tutorial 1.1

INSERT INTO Level_Map (ID, rowMap)
VALUES	(1, '+++#######'),
		(2, '####     #'),
		(3, '#        #'),
		(4, '#        #'),
		(5, '#        #'),
		(6, '####     #'),
		(7, '+++#######');


-- Fill in the enemies for Tutorial 1.1

INSERT INTO Level_Enemies (ID, eType, eXinPos, eYinPos, eRin, eMoves, eRotates)
VALUES	(1, 'Guard', 6, 3, 0, 'N', 'N');


-- Fill in the traps for Tutorial 1.1
/*
INSERT INTO Level_Traps (ID, tType, tXinPos, tYinPos, tRin)
VALUES	(1, 'Guard', 3, 6, 0);
*/