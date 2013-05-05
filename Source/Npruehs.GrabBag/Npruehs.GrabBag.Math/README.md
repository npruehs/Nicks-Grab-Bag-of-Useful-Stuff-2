Math
====

The `Npruehs.GrabBag.Math` namespace contains utitiliy classes for many
math operations.

`Math2` is a useful extension of the .NET `System.Math` class, providing
constants like `PiOverTwo`, or methods like `DegreesToRadians` for converting
angles and passing them to the .NET trigonometric functions.

John Carmack himself advises to use vector structs instead of fields for
x- and y-coordinates, for example. Thus, other parts of this Grab Bag follow
that advice, and you can do so as well. The vector structs are immutable and
feature a lot of convenient operator overloads.

> __John Carmack @ID_AA_Carmack 9:15 PM - 5 Mar 13__

> I advocate the use of an idVec2i type for all two integer element tasks over
discrete x/y, width/height, etc. A lot of code is improved.

Additionally, there are geometry classes for rectangles and boxes for the same
reason.
