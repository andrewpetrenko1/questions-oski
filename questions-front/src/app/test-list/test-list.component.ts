import { Component, OnInit } from '@angular/core';
import { Question } from '../interfaces/question';
import { QuestionService } from '../services/question.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { DisplayRes } from '../interfaces/displayRes';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.sass']
})
export class TestListComponent implements OnInit {
  listOfQuestions: [Question[]] = [[]];
  confirmCheck = false;

  results: DisplayRes[] = [];

  constructor(
    private questionService: QuestionService,
    private modalService: NgbModal,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.questionService.getList().subscribe(data => this.listOfQuestions = data);
    this.results = this.questionService.questionsResults;
  }

  getQuestionResult(index: number) {
    return this.results.find(r => r.questionIndex === index)?.result || '';
  }

  startTest(index: number, modal: any) {
    let modalRef = this.modalService.open(modal, { centered: true, size: 'sm' });
    modalRef.result.then((result) => {
      if(this.confirmCheck === true) {
        this.questionService.quetionsToAnswerId = index;
        this.router.navigate(['test-answer']);
      }
      this.confirmCheck = false;
    }).catch((err) => {
      this.confirmCheck = false;
    });
  }

}
