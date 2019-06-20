/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.mycompany.unicafe;

import org.junit.Before;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author sofvanh
 */
public class KassapaateTest {
    
    Kassapaate kassapaate;
    Maksukortti rahakasKortti;
    Maksukortti rahatonKortti;
    
    @Before
    public void setUp() {
        kassapaate = new Kassapaate();
        rahakasKortti = new Maksukortti(1000);
        rahatonKortti = new Maksukortti(10);
    }
    
    @Test
    public void alussaRahatOikein() {
        assertEquals(100000, kassapaate.kassassaRahaa());
    }
    
    @Test
    public void alussaMyytyjenEdullistenMaaraOikein() {
        assertEquals(0, kassapaate.edullisiaLounaitaMyyty());
    }
    
    @Test
    public void alussaMyytyjenMaukkaidenMaaraOikein() {
        assertEquals(0, kassapaate.maukkaitaLounaitaMyyty());
    }
    
    @Test
    public void kateisostoRahamaaraKasvaa() {
        kassapaate.syoEdullisesti(300);
        assertEquals(100240, kassapaate.kassassaRahaa());
    }
    
    @Test
    public void kateisostoVaihtorahaOikein() {
        assertEquals(100, kassapaate.syoMaukkaasti(500));
    }
    
    @Test
    public void kateisostoMyytyjenMaaraKasvaa() {
        kassapaate.syoEdullisesti(300);
        kassapaate.syoEdullisesti(1000);
        assertEquals(2, kassapaate.edullisiaLounaitaMyyty());
    }
    
    @Test
    public void kateisostoEiVaraaEdulliseenVaihtoraha() {
        assertEquals(10, kassapaate.syoEdullisesti(10));
    }
    
    @Test
    public void kateisostoEiVaraaMaukkaaseenVaihtoraha() {
        assertEquals(200, kassapaate.syoMaukkaasti(200));
    }
    
    @Test
    public void kateisostoEiVaraaEiMuutostaSaldossa() {
        int saldoAlussa = kassapaate.kassassaRahaa();
        kassapaate.syoEdullisesti(100);
        kassapaate.syoMaukkaasti(300);
        assertEquals(saldoAlussa, kassapaate.kassassaRahaa());
    }
    
    @Test
    public void kateisostoEiVaraaEiMuutostaMyydyissa() {
        kassapaate.syoEdullisesti(100);
        assertEquals(0, kassapaate.edullisiaLounaitaMyyty());
    }
    
    @Test
    public void korttiostoVoiOstaaEdullisen() {
        assertTrue(kassapaate.syoEdullisesti(rahakasKortti));
    }
    
    @Test
    public void korttiostoVoiOstaaMaukkaan() {
        assertTrue(kassapaate.syoMaukkaasti(rahakasKortti));
    }
    
    @Test
    public void korttiostoSummaVeloitetaan() {
        kassapaate.syoEdullisesti(rahakasKortti);
        assertEquals(760, rahakasKortti.saldo());
    }
    
    @Test
    public void korttiostoMyytyjenMaaraKasvaa() {
        kassapaate.syoEdullisesti(rahakasKortti);
        assertEquals(1, kassapaate.edullisiaLounaitaMyyty());
    }
    
    @Test
    public void korttiostoEiRahaaEiOstoaEdullinen() {
        assertFalse(kassapaate.syoEdullisesti(rahatonKortti));
    }
    
    @Test
    public void korttiostoEiRahaaEiOstoaMaukas() {
        assertFalse(kassapaate.syoMaukkaasti(rahatonKortti));
    }
    
    @Test
    public void korttiostoEiRahaaEiMuutostaKortillaEdullinen() {
        kassapaate.syoEdullisesti(rahatonKortti);
        assertEquals(10, rahatonKortti.saldo());
    }
    
    @Test
    public void korttiostoEiRahaaEiMuutostaKortillaMaukas() {
        kassapaate.syoMaukkaasti(rahatonKortti);
        assertEquals(10, rahatonKortti.saldo());
    }
    
    @Test
    public void korttiostoEiRahaaEiMuutostaMyydyissa() {
        kassapaate.syoEdullisesti(rahatonKortti);
        assertEquals(0, kassapaate.edullisiaLounaitaMyyty());
    }
    
    @Test
    public void korttiostoEiMuutostaKassassa() {
        int saldoAlussa = kassapaate.kassassaRahaa();
        kassapaate.syoMaukkaasti(rahakasKortti);
        kassapaate.syoEdullisesti(rahakasKortti);
        assertEquals(saldoAlussa, kassapaate.kassassaRahaa());
    }
    
    @Test
    public void latausToimii() {
        kassapaate.lataaRahaaKortille(rahatonKortti, 10);
        assertEquals(20, rahatonKortti.saldo());
    }
    
    @Test
    public void latausKassanSaldoKasvaa() {
        int saldoAlussa = kassapaate.kassassaRahaa();
        int muutos = 10000;
        kassapaate.lataaRahaaKortille(rahakasKortti, muutos);
        assertEquals(saldoAlussa + muutos, kassapaate.kassassaRahaa());
    }
    
    @Test
    public void latausEiVoiNegatiivista() {
        kassapaate.lataaRahaaKortille(rahatonKortti, -10);
        assertEquals(10, rahatonKortti.saldo());
    }
    
}
