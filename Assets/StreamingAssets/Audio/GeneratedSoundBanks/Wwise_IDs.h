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
        static const AkUniqueID RAD_PLAY = 245047015U;
        static const AkUniqueID RAD_STOP = 3233679413U;
        static const AkUniqueID TEMP_PLAY = 2832289476U;
        static const AkUniqueID TEMP_STOP = 1309769406U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace PRIORITIES
        {
            static const AkUniqueID GROUP = 1168098437U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID PRIORITY1 = 2616570358U;
                static const AkUniqueID PRIORITY2 = 2616570357U;
                static const AkUniqueID PRIORITY3 = 2616570356U;
            } // namespace STATE
        } // namespace PRIORITIES

        namespace SCANNINGSTATUS
        {
            static const AkUniqueID GROUP = 212056678U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID SCAN_IDLE = 3361303275U;
                static const AkUniqueID SCAN_RISK = 3754859140U;
            } // namespace STATE
        } // namespace SCANNINGSTATUS

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID GASLEVEL = 423230026U;
        static const AkUniqueID RADLEVEL = 994322176U;
        static const AkUniqueID TEMPLEVEL = 2582419067U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID ALISON_SOUNDSET = 785106775U;
        static const AkUniqueID JOE_SOUNDSET = 382517303U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID REVERBS = 3545700988U;
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
