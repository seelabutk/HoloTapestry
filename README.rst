============
HoloTapestry
============

This repository showcases the code required to integrate Tapestry
Hyperimages with Unity for the HoloLens. Where a browser would normally
request one image, the HoloLens must request one for each eye.


Unity Setup
-----------

To get this running in Unity, we need to do a few steps.

First, we need to make sure we have separate cameras for the two
eyes. These cameras should be set to only render things on the (newly
created) layers Left and Right.

Then we need to create a 3D Empty wrapper object which will be used to
position the hyperimage in the real world. This should have two children:
a left canvas and a right canvas. These canvases need to be set to the
Left and Right layers, respectively.

Then, under each canvas, we need a Raw Image object.

Finally, add the Script component that points to the TapestryHyperimage
script. Then drag each of the objects in the scene to the components
public variables.


Server Setup
------------

We need to make sure the configurations used for the Tapestry Server have either a transparent or a pure black background (which gets rendered as transparent on the HoloLens). This corresponds to a configuration like::

  {
    ...
    "background": [0, 0, 0],
    ...
  }
