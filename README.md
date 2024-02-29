# Randomizer For Needy Streamer Overload

![2024-02-18 11-51-39 mp4_snapshot_00 09 682](https://github.com/amazeedaizee/NeedyGirlRandomizer/assets/131136866/cc02e525-9ee7-4eaf-bdeb-c35d33d73431)

In the game, while managing each day throughout Ame's life, have you ever thought "Wow, I wish things could be more random?"

Well now you're in luck! Instead of the usual, this mod randomizes things such as text messages and tweets, so instead of Ame tweeting about going out after going out, she might tweet about gaming instead! 

In addition, this mod is customizable, so rather then Ame sending a random tweet, you might want to randomize the music that plays in the game instead! 

## Important Notes

- You must have BepInEx pre-configured and enabled with the game to use this mod. 
- Steam Achievements for this game are disabled while the mod is installed.
- The randomizer doesn't affect the Login/Caution screen, nor does it affect the Data0 save.

-----

## Randomizer Options

**You can choose what to randomize in the game by using the Settings executable that comes with the mod. Any option that is already enabled out of the box is labelled with _(Default)_.**

#### Randomize Stats (default)

Randomizes stat changes from actions, some events, ignoring DM's and deleting stressful chat comments. 

- Follower stat changes can range from -1000000 to 9999999 (dependent on how much followers you currently have)
- Stress stat changes can range from -30 to +30.
- Affection and Darkness stat changes can range from -20 to +20.

#### Include Skipped Stream Penalty (default)

If enabled, has a chance for followers to decrease per skipped stream at night, depending on current stress and followers. The lower the stress and/or the higher the followers, the higher chance of followers decreasing.

#### Randomize Jine Text

Randomizes JINE messages sent from Ame, and obscures options and text messages sent by P-chan.

(Option results and custom messages are not affected.)

#### Randomize Search Results and /st/ text

Randomizes search results from Vanity Search and /st/ text

#### Randomize Tweets

Randomizes tweets sent by KAngel and Ame. KAngel tweet likes and retweets are also randomized.

#### Randomize Tweet Replies

Randomizes tweet replies sent to KAngel. Up to seven replies can show per tweet.

#### Randomize Stream Text

Randomize stream text for every stream. Only affects KAngel's dialogue and set chat comments.

#### Randomize Diary/Logs

Randomizes Diary/Logs from Ame... and probably some other important text. Text shown is based on "notepadTexts.json" (comes with the mod per release). You can edits the contents of this file in the settings executable.

**Contents of notepadTexts.json may contain strong language.**

#### Randomize Ending Messages

Randomizes ending messages that appear in the ending dialog after getting some endings. Text shown is based on "endMsgsTexts.json" (comes with the mod per release). You can edits the contents of this file in the settings executable.

**Contents of endMsgsTexts.json may contain strong language.**

#### Randomize Special Borders

Has a chance for a special border to appear at the start of each day. (This does not include the border that appears at Internet Runaway Angel.)

#### Randomize Day Borders

Has a chance for either the time (noon, dusk, night) borders to change to another time border, or not change at all. The border change is purely visual, the actual times of day are not randomized. (You can see what time of day you're on in the bottom right corner of the screen.)

#### Randomize Visual Effects

Has a chance for a trippy visual effect to appear at the start of the day.

#### Randomize Music

Randomizes music from the game.

#### Randomize Sound Effects

Randomize sound effects from the game.
(If you want to keep your sanity, do NOT turn this option on. Seriously, don't. I mean seriously... don't. If you still choose to turn this option on anyway, well, don't say I didn't warn you.)

#### Randomize Days

Randomize days in the game. Only takes effect when going to the next day. (Might be unstable.)

#### Randomize Streams

Randomizes non-ending streams that play at night.

#### Include Special Streams

Includes the chance of playing an ending or horror stream if 'Randomize Streams' is on.

#### Randomize Animations

Randomize animations that play in the Webcam.
(Does not randomize animations in streams that use the dark streaming UI, and excludes any animations that play in said streams.)

#### Include Ame and KAngel Animations Together

Includes the chance of playing either Ame or KAngel animations when playing through the Webcam if 'Randomize Animations' is on.
If this option is off, the two types are played depending on the current event (Ame: off-stream, KAngel: on-stream).

## Other

Credits to zonni for their [post on StackOverflow]( https://stackoverflow.com/a/77701064) which helped a bit with modding this game.

Also huge thanks to [Graphene_](https://www.speedrun.com/users/Graphene_?view=fullgame), the main moderator for Needy Streamer Overload on speedrun.com, for helping me with playtesting and some of the ending messages.

This mod uses the [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) library for storing settings and text data. 
[License can be found here](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md)

This mod also uses the [Eto](https://github.com/picoe/Eto?tab=readme-ov-file) library for creating the executable.
[License can be found here](https://github.com/picoe/Eto/blob/develop/LICENSE.txt)

----

This repository and the mod itself is licensed with GPLv3.

This mod is fan-made and is not associated with xemono and WSS Playground. All properties belong to their respective owners.

Haven't downloaded Needy Streamer Overload yet? Get it here: [https://store.steampowered.com/app/1451940/NEEDY_STREAMER_OVERLOAD/](https://store.steampowered.com/app/1451940/NEEDY_STREAMER_OVERLOAD/)

