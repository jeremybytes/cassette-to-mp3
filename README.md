# cassette-to-mp3

Work in progress to convert audio cassettes to MP3 files. Both a combined application and separate applications for each step are available. This is a work-in-progress.

*Note: These projects use NAudio for recording, editing, and converting.*

## Projects
1. WAVRecorder 
Desktop application to record from a PC audio source (such as LineIn) to a WAV file.  
I use this to record a side of a cassette tape from and an Akai HX-1C through a PC input.

2. RecordingToMP3  
A desktop application that splits the recording (from WAVRecorder) into separate tracks/files and converts them to MP3 with appropriate tags.  
*Note: Double-click the "input" box to open a file dialog to pick the WAV source.*  
The output creates folders for artist and album under the original source folder.

3. AudioProcessorLibrary  
Combined library to support entire workflow (used by RecordingToMP3).  

4. WAVSplitter  
Desktop application to split a WAV file into separate files based on provided timestamps.  
I use this to split a recording of a full side of a cassette tape into individual tracks.  
*Note: I get the timestamps by looking at the WAV file in an audio editor.*  
This functionality is included in "RecordingToMP3".  

5. WAVtoMP3  
Desktop application to convert the separate wave files into MP3s based on provided track names.  
This functionality is included in "RecordingToMP3".  

6. MP3Tagger
Desktop application to add ID3 tags to MP3 files.  
This functionality is included in "RecordingToMP3".  

7. TesterApp  
Console application to test the workflow on a single, hard-coded file. (working!)

## Enhancements
I've got the following features planned.  

* Combination of splitting and converting to MP3 as a single application. **DONE**  
* Add MP3 tags (artist, album, track, year, etc.) **DONE**  
* See if I can figure out a way to "auto-split" (although I'm not sure I would trust this since albums vary in moving from track to track).  

## Notes  
This started because I got a portable boom box that converted tapes to MP3. The quality was truly awful (and the MP3s were encoded strangely). This inspired me to hook up my good cassette deck to my PC to get better recordings, and possibly run them through audio editing software before converting them to MP3s.