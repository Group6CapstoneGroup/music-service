INSERT INTO 
    music(record_number,artist, track, album, playlist)
VALUES

(1,'Eminem','Till I Collapse','The Eminem Show','workout'), 
(2,'Heart','Barracuda','Little Queen','workout'), 
(3,'Def Leppard','Hysteria','Hysteria','workout'), 
(4,'Def Leppard','Love Bites','Hysteria','workout'), 
(5,'Kanye West','Stronger','Graduation','workout'), 
(6,'AC/DC','Back In Black','Back In Black','workout'), 
(7,'Journey','Separate Ways (Worlds Apart)','Frontiers','workout'), 
(8,'Sum 41','In Too Deep','All Killer, No Filler','workout'), 
(9,'Ratt','Round and Round','Out Of The Cellar','workout'), 
(10,'Nirvana','Smells Like Teen Spirit','Nevermind','workout'),
(11,'John Lennon','Imagine','Imagine','workout'),
(12,'U2','One','Achtung Baby','workout'),
(13,'Fleetwood Mac','Dreams','Rumours','workout'),
(14,'Queen','Bohemian Rhapsody','A Night at the Opera','workout'),
(15,'Madonna','Like a Virgin','Like a Virgin','workout')
RETURNING *;