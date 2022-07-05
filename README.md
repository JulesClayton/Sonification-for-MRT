# Sonification-for-MRT

Test environment

The environment consists of a plane with voxel digital twins of real world objects. The current layout is fairly simple, but the objects can be copied, rotated etc to create a more complex one.

Translucent spheres are used to represent areas of data, currently there are 3 example spheres which can be cloned and modified as needed (turn off the mesh renderer to make them invisible when needed). Each sphere has a data type allocated to it by means of a tag (currently there are only radiation and temperature areas but creating more types is trivial). Each sphere has a script on it that generates a floating point data value based on the proximity of the closest point on the bounds of a capsule collider* to the centre of that sphere. This script simulates there being a source at the centre of the sphere so the data value decreases with distance from the centre.

*The script searches the scene for a capsule collider so that all data spheres are measuring from the same object without needing to modify which object this is in a public variable on every instance of the script. It is therefore important there is only one capsule collider active in the scene. 

Currently there is a test capsule which can be moved in the scene view while the project is running to allow testing. It has a script 'getEnvironmentData.cs' which grabs all the data from the sources in the scene and stores them in a dictionary. That script has a public array of all the tags for the different data types, so if data types are added then the list needs updating in the inspector. It also has a public list used to display data for testing purposes, currently this displays all data recorded in the scene, but is easy to modify. This script currently does a tiny amount of data processing (setting negative data values to 0) but is easy to modify to add more, or the data can be fed to another script for modification. In either case the processed values can be fed into the sonification system.

There is a SteamVR player prefab currently inactive in the scene that has a capsule collider for the player, to which I've added the data grabbing script. I will be testing the VR integration soon, but it might be a bit delayed by the paper deadline I have looming.
