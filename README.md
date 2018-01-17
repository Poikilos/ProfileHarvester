# ProfileHarvester
Get (only relevant) files all those random users saved to that business or school computer.
formerly ProfileHarvester

## Changes
(2018-01-16)
* Made the filter for files in root configurable (via new LocalProfileMasks text box) and expanded defaults (formerly dxf, py, pyw, html, htm, doc, docx, blend, blend1, prel, dxf~, prfpset)
  (dxf~ are temp files leftover by LibreCAD that could be helpful if someone forgot to save or LibreCAD or computer failed before save)
* improved status output
(2017-09-26)
* write exceptions to lbNow using this to-one-line code: string sNew = Regex.Replace(sOld, @"\t|\n|\r", "");
* eliminate dependency on RetroEngine
(2016-06-02)
* SyncFolder Should handle failure to access source folder (added out int is_ok) and not append rd /s /q for that folder nor do .Delete(true) on it (which, before this was fixed, would ignore exception that occured in SyncFolder recursive call then outer SyncFolder method would try to remove system-created NTFS symlinks such as "My Music")
* Adobe Premiere Elements 10:
  * possibly integrate PrelMover into this that changes local paths within XML to destination where ProfileHarvester put it
  * Recover to sHomeRootThenSlash+sUserFolderNow_Name+@"\Recovered Files\"+sComputerNameNow+"\Documents"
    OR Merge (MOVE THIS method to USERPROGWRAP): if NEWER folder, then
    @"C:\Documents and Settings\"+sUserNameNow+@"\Application Data\Adobe\Premiere Elements\10.0"
    and implement in UserProgWrap
* foreach NEWER file:
  DEFAULT ADOBE PREMIERE ELEMENTS 10 save folder:
  USERPROFILE, "\Documents\Adobe\Premiere Elements\10.0"
  to
  HOMES_ROOT, sUserNameNow, @"\\Recovered Files\\Adobe\\Premiere Elements\\10.0"
* for consisency, instead of saving Documents directly to Recovered Files,
  CHANGE move Documents to home drive so it goes from
  %USERPROFILE%\Documents`
  to
  Recovered Files\Documents`
* resolved by requiring elevation (apparently)
  * Delete C:\tmp\*.* at 8:15am or run with elevated privileges if missed
* (added bResetStats boolean so for multiple ForEachUser* calls, stats are only cleared on first one) Fix glitch where always says "0 user(s)" have files
* so all other recovery is same structure, use Recovered Files as equivalent to USERPROFILE during transfer, except add junk from C:\tmp to "Recovered Files" as well
  * change tmp (same for UserProgWrap)
  * change Temp (same for UserProgWrap)
(2012-05-16)
* On Windows 7, admin priveleges are required for accessing registry, so compling needs new manifest with requestedPriveleges section with
  ```xml
  <requestedExecutionLevel level="requireAdministrator"/>
  ```
* Since this is required, "runas" behavior in program may or may not be needed, but its there.


## Known Issues
* Remove option for Mortimer the Lepidopterist stats (no longer needed)
* Instead of collecting PNG sequences, use ffmpeg to create `"PNGs Recovered by FileHarvester " + sDateSuffix + ".avi"`
  (do this in ForEachFile_DoActions_ONLYMOVESFILESINTMPSOFAR,
  then delete all of the pngs,
  change fiNow to the FileInfo of the new AVI,
  then continue)
* RENAME IF EXISTS!!! (new version behaves like ForwardFileSync, losing untitled files!--correct this or make optional)
* Always delete the fluid bakes (see expertmm's personal test network: 2011-10-31 (fluidsurface_final_0174.bvel.gz etc))
* deal with randomly-named temp folders (created on each load) in temp created by blender
* Say how many files were moved, & list whom for each group of files

### Needs Testing
* (done?) change destination of My Documents to sHomeRootThenSlash + sUserFolderNow_Name + @"\Recovered Files\USERPROFILE\Documents"


## Developer Notes

### Default save folder for various programs
* DEFAULT PHOTOSHOP CS4 & FLASH CS4 save folder:
  Path.Combine(USERPROFILE, "Documents")
* GIMP and LibreCAD have link (in save menu) to USERPROFILE which is not where anyone knows how to get back to later (Documents link is there too, but average user doesn't know the difference--USERPROFILE shouldn't be there so ProfileHarvester makes up for failures of elite GNU programmers to do UX for the average person)

### Deprecated
* Recover @"%ALLUSERSPROFILE%\Documents\Projects\UserProgWrap\bin\" -- contains folders containing untitled.blend if user typed a name in the first box when saving in blender
  TO "T:\" + SafeOwner(diSubFolderInBin) + "\Recovered files\Blender Foundation Blender"

#### Tasks to move to other projects
* deduplication
* Tasks for start of day (in lieu of logoff script):
  send Blender a save signal
  close Blender nicely
  wait for UserProgWrap.exe to exit
  logout
* For each user, change desktop to enforced wallpaper manufacturer-provided wallpaper is current one
  e.g. if `"%USERPROFILENOW%\Local Settings\Application Data\Microsoft\Wallpaper1.bmp"` is same as HP Wallpaper AFTER UNCOMPRESSED
  MUST compare pixel by pixel since Windows automatically decompresses wallpaper of any type to Wallpaper1.bmp
  For each user,
  replace:
  %USERPROFILE%\Application Data\Microsoft\Wallpaper1.bmp
  with an enforced wallpaper
  if is identical to C:\WINDOWS\HPQ800h.bmp (hp wallpaper)
