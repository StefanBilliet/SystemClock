using System;
using System.Threading.Tasks;
using Xunit;

namespace SystemClock.Tests;

public class SystemClockTests
{
    private readonly SystemClockTester _sut;

    public SystemClockTests()
    {
        Clock.Reset();
        _sut = new SystemClockTester();
    }
        
    [Fact]
    public void WHEN_Set_THEN_Now_is_fixed()
    {
        var now = new DateTime(2021,1,1);
        Clock.Set(now);
            
        Assert.Equal(now, _sut.Now());
    }
        
    [Fact]
    public async Task WHEN_Set_THEN_NowAsync_is_fixed()
    {
        var now = new DateTime(2021,1,2);
        Clock.Set(now);
            
        Assert.Equal(now, await _sut.NowAsync());
    }
        
    [Fact]
    public void WHEN_Reset_THEN_Now_is_not_fixed()
    {
        Clock.Set(new DateTime(2021,1,2));
        Clock.Reset();

        var now = _sut.Now();
        var newNow = _sut.Now();
            
        Assert.NotEqual(now, newNow);
        Assert.NotEqual(new DateTime(2021,1,2), now);
        Assert.NotEqual(new DateTime(2021,1,2), newNow);
    }
        
    [Fact]
    public async Task WHEN_Reset_THEN_NowAsync_is_not_fixed()
    {
        Clock.Set(new DateTime(2021,1,2));
        Clock.Reset();

        var now = await _sut.NowAsync();
        var newNow = await _sut.NowAsync();
            
        Assert.NotEqual(now, newNow);
        Assert.NotEqual(new DateTime(2021,1,2), now);
        Assert.NotEqual(new DateTime(2021,1,2), newNow);
    }
}