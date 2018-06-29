This is a prototype for creating a game about herding cats.

https://kevind-portfolio.blogspot.com/2016/01/unity-steering.html this guy offered up a bunch of code on steering. the orange cats in the scene are his code, the black cats are my code which uses a lot from his code. 

Use WASD and Arrow keys to move the two herders. Orange cats don't flee and (are supposed to) group up. Black cats don't group up right now and wander about an anchor point, then flee when a herder comes up to them. Next to add to them is crowding behaviour and to fix why they speed up as they wander in circles (too much correction of velocity).

My code is in the top level of scripts folder (Catmovement [which is actually player movement, CatBehaviour [which actually is cat movement], isCat [cat object identifier], isPlayer [unused]). K Drure's is in the steering folder in scripts.