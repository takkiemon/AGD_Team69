package com.mycompany.unicafe;

import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;

public class MaksukorttiTest {

    Maksukortti kortti;

    @Before
    public void setUp() {
        kortti = new Maksukortti(10);
    }

    @Test
    public void luotuKorttiOlemassa() {
        assertTrue(kortti!=null);      
    }
    
    @Test
    public void saldoAlussaOikein() {
        assertEquals(10, kortti.saldo());
    }
    
    @Test
    public void rahanLataaminenKasvattaaSaldoaOikein() {
        kortti.lataaRahaa(200);
        assertEquals(210, kortti.saldo());
    }
    
    @Test
    public void voiNostaaRahaaJosSaldoa() {
        kortti.lataaRahaa(500);
        kortti.otaRahaa(360);
        assertEquals(150, kortti.saldo());
    }
    
    @Test
    public void saldoEiMuutuJosEiSaldoa() {
        kortti.lataaRahaa(20);
        kortti.otaRahaa(40);
        assertEquals(30, kortti.saldo());
    }
    
    @Test
    public void palautusOikeinTrue() {
        assertTrue(kortti.otaRahaa(10));
    }
    
    @Test
    public void palautusOikeinFalse() {
        assertFalse(kortti.otaRahaa(100));
    }
    
    @Test
    public void toStringToimii() {
        assertEquals("saldo: 0.10", kortti.toString());
    }
}
