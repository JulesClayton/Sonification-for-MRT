/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID GAS_PLAY = 609952641U;
        static const AkUniqueID GAS_STOP = 2929912663U;
        static const AkUniqueID HAZARDFOUND = 1421903547U;
        static const AkUniqueID RAD_PLAY = 245047015U;
        static const AkUniqueID RAD_PRIORITY_HIGH_ALERT_PLAY = 662617816U;
        static const AkUniqueID RAD_PRIORITY_HIGH_ALERT_STOP = 3035065130U;
        static const AkUniqueID RAD_PRIORITY_MED_ALERT = 1820290005U;
        static const AkUniqueID RAD_STOP = 3233679413U;
        static const AkUniqueID ROOMTONE_PLAY = 3167703537U;
        static const AkUniqueID TEMP_PLAY = 2832289476U;
        static const AkUniqueID TEMP_STOP = 1309769406U;
        static const AkUniqueID TEMPBONG_PLAY = 2860255532U;
        static const AkUniqueID TEMPBONG_STOP = 535072822U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace PRIORITIES
        {
            static const AkUniqueID GROUP = 1168098437U;

            namespace STATE
            {
                static const AkUniqueID HIGH_PRIORITY_ALARM = 3816057890U;
                static const AkUniqueID MED_PRIORITY_ALERT = 3294014955U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID NORMAL = 1160234136U;
                static const AkUniqueID TOP_PRIORITY_ALARM = 3051982395U;
            } // namespace STATE
        } // namespace PRIORITIES

    } // namespace STATES

    namespace SWITCHES
    {
        namespace GASTHRESHOLD
        {
            static const AkUniqueID GROUP = 839901033U;

            namespace SWITCH
            {
                static const AkUniqueID HIGH = 3550808449U;
                static const AkUniqueID LOW = 545371365U;
            } // namespace SWITCH
        } // namespace GASTHRESHOLD

        namespace HAZARDFOUND
        {
            static const AkUniqueID GROUP = 1421903547U;

            namespace SWITCH
            {
                static const AkUniqueID IDLE = 1874288895U;
                static const AkUniqueID SCANNING = 2883831984U;
            } // namespace SWITCH
        } // namespace HAZARDFOUND

        namespace SCANMODE
        {
            static const AkUniqueID GROUP = 470169951U;

            namespace SWITCH
            {
                static const AkUniqueID AUTO = 3435608004U;
                static const AkUniqueID DEEP = 1976939195U;
            } // namespace SWITCH
        } // namespace SCANMODE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID GASLEVEL = 423230026U;
        static const AkUniqueID GASPRIORITY = 3412656224U;
        static const AkUniqueID RAD = 1047714086U;
        static const AkUniqueID RADLEVEL = 994322176U;
        static const AkUniqueID RADPRIORITY = 1385746178U;
        static const AkUniqueID TEMPLEVEL = 2582419067U;
        static const AkUniqueID TEMPPRIORITY = 3363327007U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID ALISON_SOUNDSET = 785106775U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID HAZARD_FOUND = 327731021U;
        static const AkUniqueID HEADLOCKED_HIGH_PRIORITY = 2326363985U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID NO_HAZARD = 2733854484U;
        static const AkUniqueID PRIORITY_ALERTS = 187890218U;
        static const AkUniqueID RESONANCE_1ST_ORDER = 2840845035U;
        static const AkUniqueID RESONANCE_3RD_ORDER = 2647304162U;
        static const AkUniqueID REVERBS = 3545700988U;
        static const AkUniqueID STEREO_HEADLOCKED = 1494851U;
        static const AkUniqueID TEMPPRIORITY_OUT = 2832401491U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID NUCLEARWAREHOUSEVERB = 2932930741U;
        static const AkUniqueID WAREHOUSERECESSVERB = 1362633108U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
