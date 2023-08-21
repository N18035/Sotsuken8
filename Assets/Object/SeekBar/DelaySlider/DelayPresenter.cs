using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

// public interface

namespace Ken.Delay
{
    public class DelayPresenter : Singleton<DelayPresenter>
    {
        public IObservable<Unit> OnSelectDelay => _selectDelay;
        private Subject<Unit> _selectDelay = new Subject<Unit>();
        [SerializeField] DelaySliderManager manager;
        [SerializeField] CountPresenter count;
        [SerializeField] Music _music;
        
        public void Initialize(){
            count.Reset();
            manager.Reset();
        }

        public void GO(){
            _selectDelay.OnNext(Unit.Default);
            _music.LoadTiming();
        }
    }
}

