#pragma once
#include "Ten18/Timer.h"
#include "Ten18/Util.h"
#include "Ten18/Format.h"

namespace Ten18
{
    #define Ten18_TRACER() ::Ten18::Tracer tracer(__FILE__ ## "(" ## Ten18_STRINGIFY(__LINE__) ## "): " ## __FUNCTION__)

    class Tracer
    {
    public:

        Tracer(const char msg[])
            : mMsg(msg), mTimer(Timer::StartImmediately), mStopped()
        {
            Format("%0%1 begin", std::string(sIndent * sNumSpacesPerIndent, ' '), mMsg).DebugOut();
            ++sIndent;
        }

        ~Tracer()
        {
            Stop();            
        }
        
    private:

        Tracer(const Tracer&);
        Tracer& operator = (const Tracer&);

        void Stop()
        {
            if (mStopped)
                return;
            
            mTimer.Stop();
            mStopped = true;            
            --sIndent;

            Format("%0%1 end: %2 ms", std::string(sIndent * sNumSpacesPerIndent, ' '), mMsg, mTimer.Elapsed() * 1000).DebugOut();
        }
        
        const char* mMsg;
        
        Timer mTimer;
        bool mStopped;

        static const auto sNumSpacesPerIndent = 4;
        static int sIndent;
    };
}
