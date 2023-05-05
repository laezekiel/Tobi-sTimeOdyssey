-- create all character scenes

INSERT INTO Scenes_Charcters (ID, CType, GameScene)
VALUES	(1,'Guard','res://Scenes/Characters/Villager/Enemy/Enemy.tscn');

INSERT INTO Scenes_Charcters (ID, CType, GameScene, BigSceneEdo)
VALUES	(2,'Tobi','res://Scenes/Characters/player.tscn','res://Assets/Character/Tobi/TobiBig.tres');

INSERT INTO Scenes_Charcters (ID, CType, GameScene, BigSceneFutur)
VALUES	(3,'Villager','res://Scenes/Characters/Villager/Villager.tscn','res://Assets/Character/Scientist/BigScientist.tres');

INSERT INTO Scenes_Charcters (ID, CType, BigSceneEdo)
VALUES	(4,'Sifu','res://Assets/Character/Sifu/SifuBig.tres');